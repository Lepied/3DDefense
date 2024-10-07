using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RelayManager relayManager;


    
    private static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
       
    }

    public async void OnClickHost()
    {
        // RelayManager의 StartHostWithRelay 함수 호출
        await relayManager.StartHostWithRelay();
        SceneManager.Instance.LoadScene("Lobby");
        SceneManager.Instance.UnloadScene("Main");

    }

    // 클라이언트 참가 버튼 클릭 시 호출되는 함수
    public async void OnClickJoin()
    {
        // RelayManager의 StartClientWithRelay 함수 호출
        bool isConnected = await relayManager.StartClientWithRelay();

        if (isConnected)
        {
            SceneManager.Instance.LoadScene("Lobby");
            SceneManager.Instance.UnloadScene("Main");
        }
        else
        {
            Debug.LogError("서버접속 실패");
        }

        
        
    }

    public void OnClickStart()
    {
        SceneManager.Instance.LoadScene("Game");
        SceneManager.Instance.UnloadScene("Lobby");
    }

}
