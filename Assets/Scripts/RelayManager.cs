using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FishNet.Managing;
using FishNet.Transporting.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelayManager : MonoBehaviour
{
    public static RelayManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public NetworkManager networkManager;

    public TMP_InputField joinCodeInputField;

    //public async Task<string> StartHostWithRelay(int maxConnections = 5)

    public async Task StartHostWithRelay(int maxConnections = 5)
    {
        // 유니티 서비스 초기화하기
        await UnityServices.InitializeAsync();

        //익명으로  접속
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        //릴레이 서버 생성 / maxConnections 수 만큼 인원수
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);

        //가입코드 생성
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        //가입코드 자동할당
        joinCodeInputField.text = joinCode;

        //FishNet에 들어오는 트래픽을 전부 릴레이 서버로 전달되도록 설정
        FishyUnityTransport transport = networkManager.TransportManager.GetTransport<FishyUnityTransport>();
        transport.SetRelayServerData(new RelayServerData(allocation, "dtls"));

        //호스트 시작
        if (networkManager.ServerManager.StartConnection())//호스트 시작 
        {
            networkManager.ClientManager.StartConnection();//클라이언트 시작
            //return joinCode;
        }
        //return null;
    }

    /*
    public async Task<bool> StartClientWithRelay(string joinCode)
    */
    public async Task StartClientWithRelay()
    {

        if (joinCodeInputField == null || string.IsNullOrWhiteSpace(joinCodeInputField.text))
        {
            Debug.LogError("참가 코드가 비어있습니다. 코드를 입력하세요.");
            return;
        }

        string joinCode = joinCodeInputField.text;



        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        //가입 코드로 릴레이 서버에 접속
        var joinloacation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);

        //FishNet에 들어오는 트래픽을 전부 릴레이 서버로 전달되도록 설정
        FishyUnityTransport transport = networkManager.TransportManager.GetTransport<FishyUnityTransport>();
        transport.SetRelayServerData(new RelayServerData(joinloacation, "dtls"));

        //클라이언트 시작
        networkManager.ClientManager.StartConnection();
    }
}
