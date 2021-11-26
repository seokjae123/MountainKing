using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;
    private CapsuleCollider capsuleCollider;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyController = GameObject.Find("Enemy").GetComponent<EnemyController>();
    }

    void Update()
    {
        if(enemyController.Enemy_Hp <= 0)
        {
            capsuleCollider.enabled = false;
        }
    }

    public void NPCDown()
    {
        animator.SetBool("NPCDown",true);
    }
}
