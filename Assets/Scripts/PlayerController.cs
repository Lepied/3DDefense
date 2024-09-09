using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    public float rotationSpeed = 700f;
    private PlayerController controller;
    private Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
