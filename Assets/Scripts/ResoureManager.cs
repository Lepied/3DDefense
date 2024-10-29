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
    // ��ư�� �Լ�
    public void OnAddPersonalWoodButtonPressed(int amount)
    {
        AddPersonalWood(amount); 
    }

    public void OnSpendPersonalWoodButtonPressed(int amount)
    {
        SpendPersonalWood(amount); 
    }


    //�ڿ����� ��� Ŭ���̾�Ʈ�� ����ϰ� �߰�
    //But, ���� �ڿ��� �����ڿ��� ����
    // �����ڿ� ���縦 �߰�
    [ServerRpc(RequireOwnership = false)]
    public void AddPersonalWood(int amount, NetworkConnection sender = null)
    {
        Debug.Log($"AddPersonalWood called by: {this.Owner.ClientId}");

        personalWood.Value += amount;
        UpdatePersonalWood(sender,personalWood.Value);
        Debug.Log("���� ���� ȹ�� +" + personalWood.Value);
    }


    //�����ڿ� ����Һ�
    [ServerRpc(RequireOwnership = false)]
    public void SpendPersonalWood(int amount, NetworkConnection sender = null)
    {
        if (personalWood.Value >= amount)
        {
            personalWood.Value -= amount;
            UpdatePersonalWood(sender,personalWood.Value);
            NotifyClientWoodSpent(sender,true);
        }
        else
        {
            NotifyClientWoodSpent(sender, false);
        }
    }

    [TargetRpc]
    private void UpdatePersonalWood(NetworkConnection target, int newWoodCount)
    {
        Debug.Log($"UpdatePersonalWood called for connection: {target.ClientId} with wood count: {newWoodCount}");

        InGameUIManager uiManager = FindFirstObjectByType<InGameUIManager>();
        if (uiManager != null)
        {
            uiManager.UpdatePersonalWoodUI(newWoodCount);
        }
        else
        {
            Debug.LogWarning("InGameUIManager not found.");
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
        InGameUIManager uiManager = FindFirstObjectByType<InGameUIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateSharedWoodUI(newWoodCount);
        }
    }
}
