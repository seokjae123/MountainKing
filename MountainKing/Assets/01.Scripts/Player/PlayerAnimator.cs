using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerattackCollision;
    public Animator animator;
    public float player_Hp;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    //움직임 애니메이션
    public void OnMovement(float horizontal, float vertical)
    {
        animator.SetFloat("X", horizontal);
        animator.SetFloat("Y", vertical);
    }
    //공격 애니메이션 작동
    public void IsAttack()
    {
        animator.SetTrigger("IsAttack");
    }
    //피격 애니메이션 작동
    public void IsHit()
    {
        animator.SetTrigger("OnHit");
    }
    //플레이어 체력 깎음
    public void Damaged(float PlayerHp)
    {
        animator.SetFloat("Player_HP", PlayerHp);
    }
    //플레이어 부활
    public void Resurrect(float PlayerHp)
    {
        animator.SetFloat("Player_HP", PlayerHp);
        animator.SetBool("isResurrect", false);
    }
    //공격 시 검 콜라이더 생성
    public void OnAttackCollision()
    {
        PlayerattackCollision.SetActive(true);
    }
}
