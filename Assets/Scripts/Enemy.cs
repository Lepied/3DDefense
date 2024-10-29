using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour
{
    public float speed = 2f;
    private float attackRange = 1.5f;
    public int damage = 10;

    private Transform target;
    private float attackCooldown = 2f;
    private float lastAttacktime;

    [SerializeField]
    private NavMeshAgent agent;


    void Start()
    {
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent�� �Ҵ���� �ʾҽ��ϴ�.");
        }
        agent.speed = speed;
        //agent.stoppingDistance = attackRange; //���� ��Ÿ����� ���߰��ϱ�
    }
    void Update()
    {
        if (IsServerInitialized)
        {
            FindClosestTarget();
            if(target != null)
            {
                agent.SetDestination(target.position);
                //MoveToTarget();
                CheckForAttack();
            }

        }
    }

    void FindClosestTarget()
    {
        float closestDistance = 100f; //�νĹ���(?)
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
            if(Time.time >= lastAttacktime+attackCooldown)
            {
                PlayerController playerController = target.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(damage);
                }
                lastAttacktime = Time.time;
                DespawnEnemy();
            }

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
