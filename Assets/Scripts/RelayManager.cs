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
        // ����Ƽ ���� �ʱ�ȭ�ϱ�
        await UnityServices.InitializeAsync();

        //�͸�����  ����
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        //������ ���� ���� / maxConnections �� ��ŭ �ο���
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);

        //�����ڵ� ����
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        //�����ڵ� �ڵ��Ҵ�
        joinCodeInputField.text = joinCode;

        //FishNet�� ������ Ʈ������ ���� ������ ������ ���޵ǵ��� ����
        FishyUnityTransport transport = networkManager.TransportManager.GetTransport<FishyUnityTransport>();
        transport.SetRelayServerData(new RelayServerData(allocation, "dtls"));

        //ȣ��Ʈ ����
        if (networkManager.ServerManager.StartConnection())//ȣ��Ʈ ���� 
        {
            networkManager.ClientManager.StartConnection();//Ŭ���̾�Ʈ ����
            //return joinCode;
        }
        //return null;
    }

    /*
    public async Task<bool> StartClientWithRelay(string joinCode)
    */
    public async Task<bool> StartClientWithRelay()
    {

        if (joinCodeInputField == null || string.IsNullOrWhiteSpace(joinCodeInputField.text))
        {
            Debug.LogError("���� �ڵ尡 ����ֽ��ϴ�. �ڵ带 �Է��ϼ���.");
            return false;
        }

        string joinCode = joinCodeInputField.text;



        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        //���� �ڵ�� ������ ������ ����
        var joinloacation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);

        //FishNet�� ������ Ʈ������ ���� ������ ������ ���޵ǵ��� ����
        FishyUnityTransport transport = networkManager.TransportManager.GetTransport<FishyUnityTransport>();
        transport.SetRelayServerData(new RelayServerData(joinloacation, "dtls"));

        //Ŭ���̾�Ʈ ����
        networkManager.ClientManager.StartConnection();
        return true;
    }
}
