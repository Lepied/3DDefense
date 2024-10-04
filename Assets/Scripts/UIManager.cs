using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RelayManager relayManager;

    /*
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
    */

    public async void OnClickHost()
    {
        // RelayManager의 StartHostWithRelay 함수 호출
        await relayManager.StartHostWithRelay();  // 비동기 함수이므로 따로 await를 하지 않고 그냥 호출 가능
    }

    // 클라이언트 참가 버튼 클릭 시 호출되는 함수
    public async void OnClickJoin()
    {
        // RelayManager의 StartClientWithRelay 함수 호출
        await relayManager.StartClientWithRelay(); // 비동기 함수이므로 따로 await를 하지 않고 그냥 호출 가능
    }

}
