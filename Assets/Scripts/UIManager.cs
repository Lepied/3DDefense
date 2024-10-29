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
        // RelayManager�� StartHostWithRelay �Լ� ȣ��
        await relayManager.StartHostWithRelay();
        SceneManager.Instance.LoadScene("Lobby");
        SceneManager.Instance.UnloadScene("Main");

    }

    // Ŭ���̾�Ʈ ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public async void OnClickJoin()
    {
        // RelayManager�� StartClientWithRelay �Լ� ȣ��
        bool isConnected = await relayManager.StartClientWithRelay();

        if (isConnected)
        {
            SceneManager.Instance.LoadScene("Lobby");
            SceneManager.Instance.UnloadScene("Main");
        }
        else
        {
            Debug.LogError("�������� ����");
        }

        
        
    }

    public void OnClickStart()
    {
        SceneManager.Instance.LoadScene("Game");
        SceneManager.Instance.UnloadScene("Lobby");
        SceneManager.Instance.UnloadScene("Main");
    }

}
