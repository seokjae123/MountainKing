using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    public GameObject rock;

    [SerializeField]
    private GameObject BossLeftHand;
    [SerializeField]
    private GameObject BossRightHand;
    public Animator animator;
    public bool playernear = false;
    public AudioSource bossHit;
    void Start()
    {
        animator = GetComponent<Animator>();       
    }
    public void PlayerNear()
    {
        animator.SetBool("PlayerNear", true);
        playernear = true;
    }
    public void PlayerFar()
    {
        animator.SetBool("PlayerNear", false);
        playernear = false;
    }

    public void OnHit()
    {
        bossHit.Play();
        animator.SetTrigger("BossOnHit");
    }

    public void PlayerDead()
    {
        animator.SetBool("PlayerDead", true);
    }
    public void Damaged(float BossHp)
    {
        animator.SetFloat("BossHp", BossHp);
    }

    public void Movement(float velocity) //0.2초 동안 콜라이더 생성
    {
        animator.SetFloat("BossMoveSpeed", velocity);
    }

    public void BossOnAttackPunch() //0.2초 동안 콜라이더 생성
    {
        //animator.SetFloat("BossMoveSpeed", 0);
        animator.SetTrigger("BossIsAttack");
        BossRightHand.SetActive(true);
    }

    public void BossOnAttackPunch2() // 0.2초 동안 콜라이더 생성
    {
        //animator.SetFloat("BossMoveSpeed", 0);
        animator.SetTrigger("BossIsAttack");
        BossLeftHand.SetActive(true);
    }

    private IEnumerator MakeRock()
    {
        yield return new WaitForSeconds(1.25f);
        rock.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rock.SetActive(false);
    }

    
}

