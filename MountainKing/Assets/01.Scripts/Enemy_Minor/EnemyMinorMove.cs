using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMinorMove : MonoBehaviour
{
    private EnemyMinorAnimator enemyminorAnimator;
    public Transform SpawnPoint;
    private Transform target;
    private NavMeshAgent navAgent;
    private PlayerController player;

    public float velocity;
    public float accelaration;
    public bool IsReset;
    private bool isattack;

    public GameObject hpTarget;

    public void Awake()
    {
        enemyminorAnimator = GetComponent<EnemyMinorAnimator>();
        target = GameObject.FindWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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

        //각도 계산
        //Vector3 relativePos = target.position - transform.position;
        //Vector3 look = Vector3.Slerp(this.transform.forward, relativePos.normalized, Time.deltaTime);
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        //스폰 범위 안 일 때
        if (IsReset == false)
        {
            if (DistanceToSpawn < 20.0f)
            {
                ////범위 밖이면 멈추는 거리는 대상에서 + 2
                navAgent.stoppingDistance = 2;             

                // 범위 안에 있으면 해당 방향으로 이동
                if (DistanceToPlayer <= 20.0f && DistanceToPlayer > 2.0f)
                {
                    //this.transform.rotation = rotation;
                    accelaration = 1f;
                    navAgent.SetDestination(target.position);
                    enemyminorAnimator.Movement(velocity);
                    hpTarget.SetActive(true);
                }

                else
                {
                    if (DistanceToPlayer <= 2.0f)
                    {
                        //this.navAgent.updateRotation = false;
                        //this.navAgent.updatePosition = false;
                        enemyminorAnimator.IsAttack();
                        hpTarget.SetActive(true);

                        if (player.IsDead == true)
                        {
                            enemyminorAnimator.PlayerDead();
                        }
                    }

                    velocity = 0f;
                    enemyminorAnimator.Movement(velocity);                 
                }
            }

            else
            {
                IsReset = true;
            }
        }

        //스폰 범위 밖
        else
        {
            //범위 밖이면 멈추는 거리는 대상에서 0
            navAgent.stoppingDistance = 0;
            navAgent.SetDestination(SpawnPoint.position);
            accelaration = 10f;
            velocity = 10f;
            enemyminorAnimator.Movement(velocity);
            hpTarget.SetActive(false);

            if (DistanceToSpawn <= 0.5f)
            {
                IsReset = false;
                velocity = 0f;
                enemyminorAnimator.Movement(velocity);
            }
        }
    }
}
