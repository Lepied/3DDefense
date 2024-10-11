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
        Debug.Log("HP UI ¾÷µ¥ÀÌÆ® : " + hp);
    }

    public void UpdatePersonalWoodUI(int newWood)
    {
        personalWoodText.text = "¸ñÀç : "+ newWood.ToString();
        Debug.Log("°³ÀÎ ¸ñÀç È¹µæ +" + newWood);
    }

    public void UpdateSharedWoodUI(int newWood)
    {
        sharedWoodText.text = "¸ñÀç : " + newWood.ToString();
        Debug.Log("°ø¿ë ¸ñÀç È¹µæ +" + newWood);
    }


}
