using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Slider playerHPbar;

    public void UpdateHPBar(int hp)
    {
  
        playerHPbar.value = (float)hp / 100f;
        Debug.Log("HP UI 업데이트 : " + hp);
    }
}
