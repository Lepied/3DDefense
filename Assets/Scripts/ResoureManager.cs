using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoureManager : NetworkBehaviour
{
    public readonly SyncVar<int> personalWood = new SyncVar<int>(); //개인 자원
    public readonly SyncVar<int> sharedWood = new SyncVar<int>(); //공용 자원
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
        // 초기값 설정
        personalWood.Value = 0; 
        sharedWood.Value = 0;
    }
    // 버튼용 함수
    public void OnAddPersonalWoodButtonPressed(int amount)
    {
        AddPersonalWood(amount); 
    }

    public void OnSpendPersonalWoodButtonPressed(int amount)
    {
        SpendPersonalWood(amount); 
    }


    //자원들은 모든 클라이언트가 사용하고 추가
    //But, 개인 자원과 공용자원이 별도
    // 개인자원 목재를 추가
    [ServerRpc(RequireOwnership = false)]
    public void AddPersonalWood(int amount, NetworkConnection sender = null)
    {
        Debug.Log($"AddPersonalWood called by: {this.Owner.ClientId}");

        personalWood.Value += amount;
        UpdatePersonalWood(sender,personalWood.Value);
        Debug.Log("개인 목재 획득 +" + personalWood.Value);
    }


    //개인자원 목재소비
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

        InGameUIManager uiManager = FindObjectOfType<InGameUIManager>();
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
        // 클라이언트에서 자원 소비 성공/실패 여부에 따라 처리
        if (isSuccess)
        {
            Debug.Log("자원 소비 성공");
        }
        else
        {
            Debug.Log("자원이 부족하여 소비할 수 없습니다.");
        }
    }

    // 공용 자원에 목재를 추가
    [ServerRpc(RequireOwnership = false)]
    public void AddSharedWood(int amount)
    {
        sharedWood.Value += amount;
        UpdateSharedWoodOnClients(sharedWood.Value);
    }

    // 공용 자원의 UI를 업데이트
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
