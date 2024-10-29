using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Slider playerHPbar;

    [SerializeField]
    private Text personalWoodText;
    [SerializeField]
    private Text sharedWoodText;

    public void UpdateHPBar(int hp)
    {
  
        playerHPbar.value = (float)hp / 100f;
        Debug.Log("HP UI ������Ʈ : " + hp);
    }

    public void UpdatePersonalWoodUI(int newWood)
    {
        personalWoodText.text = "���� : "+ newWood.ToString();
        Debug.Log("���� ���� ȹ�� +" + newWood);
    }

    public void UpdateSharedWoodUI(int newWood)
    {
        sharedWoodText.text = "���� : " + newWood.ToString();
        Debug.Log("���� ���� ȹ�� +" + newWood);
    }


}
