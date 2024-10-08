using FishNet.Example.ColliderRollbacks;
using FishNet.Object;
using FishNet.Object.Synchronizing;
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
    private Vector3 cameraOffset = new Vector3(0, 15, -9); // ī�޶� ��ġ ������
    private Camera playerCamera;


    public readonly SyncVar<int> hp = new SyncVar<int>();
    private int playerId; // �÷��̾� ID ����


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
            playerCamera.transform.rotation = Quaternion.Euler(60, 0, 0); //�������
        }
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        hp.Value = 100; // �ʱ� ü�°� ����
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
            playerCamera.transform.position = transform.position + cameraOffset; // ī�޶� ��ġ ������Ʈ
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            direction = Vector3.zero;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamage(int damage)
    {
        hp.Value -= damage;
        Debug.Log("���� ü�� : "+ hp.Value);
        if(hp.Value<=0)
        {
            Debug.Log($"�÷��̾� ID: {this.NetworkObject.OwnerId} ���");
        }
        UpdateHPOnClient(hp.Value);
    }

    [ObserversRpc]
    private void UpdateHPOnClient(int currentHP)
    {
        InGameUIManager uiManager  = FindObjectOfType<InGameUIManager>();
        if(uiManager != null) 
        {
            uiManager.UpdateHPBar(currentHP);
        }
    }
}

