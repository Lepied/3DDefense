using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoureManager : NetworkBehaviour
{
    public readonly SyncVar<int> personalWood = new SyncVar<int>(); //���� �ڿ�
    public readonly SyncVar<int> sharedWood = new SyncVar<int>(); //���� �ڿ�
    public static ResoureManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        // �ʱⰪ ����
        personalWood.Value = 0; 
        sharedWood.Value = 0;
    }

    //�ڿ����� ��� Ŭ���̾�Ʈ�� ����ϰ� �߰�
    //But, ���� �ڿ��� �����ڿ��� ����
    // �����ڿ� ���縦 �߰�
    [ServerRpc(RequireOwnership = false)]
    public void AddPersonalWood(int amount)
    {
        personalWood.Value += amount;
        UpdatePersonalWood(this.Owner,personalWood.Value);
    }


    //�����ڿ� ����Һ�
    [ServerRpc(RequireOwnership = false)]
    public void SpendPersonalWood(int amount)
    {
        if (personalWood.Value >= amount)
        {
            personalWood.Value -= amount;
            UpdatePersonalWood(this.Owner,personalWood.Value);
            NotifyClientWoodSpent(this.Owner,true);
        }
        else
        {
            NotifyClientWoodSpent(this.Owner,false);
        }
    }
    [TargetRpc]
    private void UpdatePersonalWood(NetworkConnection target, int newWoodCount)
    {
        InGameUIManager uiManager = FindObjectOfType<InGameUIManager>();
        if (uiManager != null)
        {
            uiManager.UpdatePersonalWoodUI(newWoodCount);
        }
    }
    [TargetRpc]
    private void NotifyClientWoodSpent(NetworkConnection target,bool isSuccess)
    {
        // Ŭ���̾�Ʈ���� �ڿ� �Һ� ����/���� ���ο� ���� ó��
        if (isSuccess)
        {
            Debug.Log("�ڿ� �Һ� ����");
        }
        else
        {
            Debug.Log("�ڿ��� �����Ͽ� �Һ��� �� �����ϴ�.");
        }
    }

    // ���� �ڿ��� ���縦 �߰�
    [ServerRpc(RequireOwnership = false)]
    public void AddSharedWood(int amount)
    {
        sharedWood.Value += amount;
        UpdateSharedWoodOnClients(sharedWood.Value);
    }

    // ���� �ڿ��� UI�� ������Ʈ
    [ObserversRpc]
    private void UpdateSharedWoodOnClients(int newWoodCount)
    {
        InGameUIManager uiManager = FindObjectOfType<InGameUIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateSharedWoodUI(newWoodCount);
        }
    }
}
