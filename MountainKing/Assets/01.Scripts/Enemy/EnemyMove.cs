using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    public Transform SpawnPoint;
    public Transform target;
    private NavMeshAgent navAgent;
    public PlayerController player;

    public float velocity;
    public float accelaration;
    public bool IsReset;
    private bool isattack;

    public GameObject hpTarget;

    public void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        IsReset = false;
    }

    void Update()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {        
        velocity = velocity + accelaration;

        // 거리 계산
        float DistanceToPlayer = Vector3.Distance(target.position, transform.position);
        float DistanceToSpawn = Vector3.Distance(SpawnPoint.position, transform.position);

        //스폰 범위 안 일 때
        if (IsReset == false)
        {
            if (DistanceToSpawn < 10.0f)
            {
                ////범위 밖이면 멈추는 거리는 대상에서 + 2
                navAgent.stoppingDistance = 2;             

                // 범위 안에 있으면 해당 방향으로 이동
                if (DistanceToPlayer <= 10.0f && DistanceToPlayer > 2.0f)
                {
                    accelaration = 1f;
                    navAgent.SetDestination(target.position);
                    enemyAnimator.Movement(velocity);
                    hpTarget.SetActive(true);
                }

                else
                {
                    if (DistanceToPlayer <= 2.0f)
                    {
                        enemyAnimator.IsAttack();
                        hpTarget.SetActive(true);

                        if (player.IsDead == true)
                        {
                            enemyAnimator.PlayerDead();
                            IsReset = true;
                        }

                        else
                        {
                            enemyAnimator.PlayerAlive();
                            IsReset = false;
                        }
                    }

                    velocity = 0f;
                    enemyAnimator.Movement(velocity);
                }
            }

            else
            {
                IsReset = true;
            }
        }

        //스폰 범위 밖
        else if (player.IsDead == true)
        {
            //범위 밖이면 멈추는 거리는 대상에서 0
            navAgent.stoppingDistance = 0;
            navAgent.SetDestination(SpawnPoint.position);
            accelaration = 10f;
            velocity = 10f;
            enemyAnimator.Movement(velocity);
            hpTarget.SetActive(false);

            if (DistanceToSpawn <= 0.5f)
            {
                IsReset = false;
                velocity = 0f;
                enemyAnimator.Movement(velocity);
            }
        }
    }
}
