using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Slider playerHPbar;

    private PlayerController player;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if(player != null && player.IsOwner) 
        {
            UpdateHPBar(player.hp.Value);
        }
        Debug.Log(player != null ? "Player found" : "Player not found");
    }

    private void OnEnable()
    {
        if (player != null && player.IsOwner)
        {

            player.hp.OnChange += OnHealthChanged;
        }
        else
            Debug.Log("OnEnable에서 플레이어없음");
    }

    private void OnDisable()
    {
        if (player != null && player.IsOwner)
        {
            player.hp.OnChange -= OnHealthChanged;
        }
        else
            Debug.Log("OnDisable에서 플레이어없음");
    }

    private void OnHealthChanged(int oldHealth, int newHealth, bool asServer)
    {
        if (player.IsOwner)  // 자신의 UI만 업데이트
        {
            UpdateHPBar(newHealth);
            Debug.Log("체력바뀜!");
        }
    }

    private void UpdateHPBar(int hp)
    {
        playerHPbar.value = (float)hp / 100f;
    }
}
