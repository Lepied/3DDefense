using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /[SerializeField]
    private RelayManager relayManager;

    [SerializeField]
    private TMP_InputField joinCodeInputField;
    [SerializeField]
    private Text statusText;


    void Start()
    {
        if(relayManager == null)
        {
            relayManager = FindObjectOfType<RelayManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
