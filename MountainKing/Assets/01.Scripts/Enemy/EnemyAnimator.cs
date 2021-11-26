using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyattackCollision;
    public Animator animator;

    public AudioSource enemyHit;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Movement(float velocity)
    {
        animator.SetFloat("EnemyMoveSpeed", velocity);
    }

    public void IsAttack()
    {
        animator.SetTrigger("EnemyIsAttack");
    }

    public void OnHit()
    {
        enemyHit.Play();
        animator.SetTrigger("EnemyOnHit");
    }

    public void Damaged(float EnemyHp)
    {
        animator.SetFloat("EnemyHp", EnemyHp);
    }

    public void PlayerDead()
    {
        animator.SetBool("PlayerDead", true);
    }
    public void PlayerAlive()
    {
        animator.SetBool("PlayerDead", false);
    }

    public void EnemyOnAttackCollision()
    {
        EnemyattackCollision.SetActive(true);
    }
    
}
