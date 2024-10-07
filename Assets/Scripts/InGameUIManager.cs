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
            Debug.Log("OnEnable���� �÷��̾����");
    }

    private void OnDisable()
    {
        if (player != null && player.IsOwner)
        {
            player.hp.OnChange -= OnHealthChanged;
        }
        else
            Debug.Log("OnDisable���� �÷��̾����");
    }

    private void OnHealthChanged(int oldHealth, int newHealth, bool asServer)
    {
        if (player.IsOwner)  // �ڽ��� UI�� ������Ʈ
        {
            UpdateHPBar(newHealth);
            Debug.Log("ü�¹ٲ�!");
        }
    }

    private void UpdateHPBar(int hp)
    {
        playerHPbar.value = (float)hp / 100f;
    }
}
