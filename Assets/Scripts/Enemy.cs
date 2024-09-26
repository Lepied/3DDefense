using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    public float speed = 2f;
    private float attackRange = 1.5f;
    public int damage = 10;

    private Transform target;
    //private float attackTime = 0;

    void Update()
    {
        if (base.IsServer)
        {
            FindClosestTarget();
            if(target != null)
            {
                MoveToTarget();
                CheckForAttack();
            }

        }
    }

    void FindClosestTarget()
    {
        float closestDistance = 100f; //인식범위(?)
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = player.transform;
            }
        }
    }

    void MoveToTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void CheckForAttack()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            Debug.Log("플레이어 체력 " + damage + "만큼 감소 됨.");
            DespawnEnemy();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DespawnEnemy()
    {
        {
            Despawn();
        }
        
    }
}
