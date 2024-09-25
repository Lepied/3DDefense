using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    public Transform target;
    public Vector3 offset;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(target != null)
        {
            transform.position = target.position + offset;
        }
        
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        cam.enabled = true;
    }

}
