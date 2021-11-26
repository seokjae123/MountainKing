using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    private BossAnimator bossAnimator;
    public Transform SpawnPoint;
    private Transform target;
    private NavMeshAgent navAgent;
    private PlayerController player;

    public float velocity;
    public float accelaration;

    public GameObject hpTarget;

    // Start is called before the first frame update
    void Awake()
    {
        bossAnimator = GetComponent<BossAnimator>();
        target = GameObject.FindWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        //IsReset = false;
    }

    // Update is called once per frame
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

        if (DistanceToSpawn < 30.0f)
        {
            //범위 밖이면 멈추는 거리는 대상에서 + 2.5
            bossAnimator.PlayerFar();
            navAgent.stoppingDistance = 3f;

                // 범위 안에 있으면 해당 방향으로 이동
            if (DistanceToPlayer <= 30.0f && DistanceToPlayer > 3f)
            {
                accelaration = 1f;
                navAgent.SetDestination(target.position);
                bossAnimator.Movement(velocity);
                hpTarget.SetActive(true);
            }

            else
            {
                if (DistanceToPlayer <= 3f)
                {
                    bossAnimator.PlayerNear();
                    hpTarget.SetActive(true);

                    if (bossAnimator.playernear == true)
                    {
                        accelaration = 0f;
                        bossAnimator.BossOnAttackPunch();
                    }

                    if (player.IsDead == true)
                    {
                        bossAnimator.PlayerDead();
                    }
                }

                velocity = 0f;
                bossAnimator.Movement(velocity);
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
            bossAnimator.Movement(velocity);
            hpTarget.SetActive(false);

            if (DistanceToSpawn <= 0.5f)
            {
                velocity = 0f;
                bossAnimator.Movement(velocity);
            }
        }
    }
}
