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
        // RelayManager�� StartHostWithRelay �Լ� ȣ��
        await relayManager.StartHostWithRelay();  // �񵿱� �Լ��̹Ƿ� ���� await�� ���� �ʰ� �׳� ȣ�� ����
    }

    // Ŭ���̾�Ʈ ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public async void OnClickJoin()
    {
        // RelayManager�� StartClientWithRelay �Լ� ȣ��
        await relayManager.StartClientWithRelay(); // �񵿱� �Լ��̹Ƿ� ���� await�� ���� �ʰ� �׳� ȣ�� ����
    }

}
