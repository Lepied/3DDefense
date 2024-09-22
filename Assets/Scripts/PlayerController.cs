using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float horizontal;
    float vertical;
    public float speed = 5f;
    private PlayerController controller;
    private Rigidbody rb;

    Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
   
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical).normalized;
    }

    void FixedUpdate()
    {
        if (direction.magnitude > 0.1f)
        {
            Vector3 move = direction * speed * Time.fixedDeltaTime;


            rb.MovePosition(rb.position + move);

            
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                rb.rotation = Quaternion.Lerp(rb.rotation, toRotation, Time.fixedDeltaTime * 10);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            direction = Vector3.zero;
        }
    }
}

