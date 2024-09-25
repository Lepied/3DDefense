using FishNet.Example.ColliderRollbacks;
using FishNet.Object;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    float horizontal;
    float vertical;
    public float speed = 5f;

    Vector3 direction;
    Rigidbody rb;

    [SerializeField]
    private Vector3 cameraOffset = new Vector3(0, 15, -9); // 카메라 위치 오프셋
    private Camera playerCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner) 
        {
            playerCamera = Camera.main;
            playerCamera.transform.position = transform.position + cameraOffset; ;
            playerCamera.transform.rotation = Quaternion.Euler(60, 0, 0); //방향고정
        }
    }


    void Update()
    {
        if (!base.IsOwner)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (playerCamera != null)
        {
            playerCamera.transform.position = transform.position + cameraOffset; // 카메라 위치 업데이트
        }

    }

    void FixedUpdate()
    {
        if (!base.IsOwner)
        {
            return;
        }

        if (direction.magnitude > 0.1f)
        {
            Vector3 move = direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);



            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.fixedDeltaTime * 10);
            }
        }

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            direction = Vector3.zero;
        }
    }
    */
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            direction = Vector3.zero;
        }
    }
}

