using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTrapMinorController : MonoBehaviour
{
    private EnemyMinorAnimator enemyminorAnimator;
    private EnemyMinorMove enemyminormove;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    private BoxCollider boxCollider;
    private TrapCheck trap_check;
    public int Enemy_Hp = 100;

    public Slider slider_Hp;
    public Slider slider_HpBack;
    public bool backHpMove = false;

    public void Update()
    {
        slider_Hp.value = Enemy_Hp;

        if (backHpMove)
        {
            slider_HpBack.value = Mathf.Lerp(slider_HpBack.value, slider_Hp.value, Time.deltaTime * 3f);
            if (slider_Hp.value >= slider_HpBack.value - 0.01f)
            {
                slider_HpBack.value = slider_Hp.value;
                backHpMove = false;
            }
        }

        if (Enemy_Hp < 100 && Enemy_Hp > 0)
        {
            if (backHpMove == false)
            {
                enemyminorAnimator.animator.SetFloat("EnemyHp", Enemy_Hp);
            }

        }
    }

    private void Awake()
    {
        enemyminorAnimator = GetComponent<EnemyMinorAnimator>();
        enemyminormove = GetComponent<EnemyMinorMove>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        boxCollider = GetComponent<BoxCollider>();
        trap_check = GameObject.Find("EventCheck").GetComponent<TrapCheck>();
    }

    public void EnemyTakeDamage(int damage)
    {
        Debug.Log("드워프 광부" + damage + "만큼 감소");
        enemyminorAnimator.OnHit();
        Enemy_Hp -= damage;
        Invoke("BackHpMove", 0.5f);
        enemyminorAnimator.Damaged(Enemy_Hp);
        StartCoroutine("OnHitColor");
        StartCoroutine("OneHit");

        if (this.Enemy_Hp <= 0)
        {
            boxCollider.enabled = false;
            enemyminormove.enabled = false;
            trap_check.trap_enemy_die += 1;
            enemyminormove.hpTarget.SetActive(false);
        }
    }

    private IEnumerator OneHit()
    {
        this.tag = "Untagged";
        yield return new WaitForSeconds(0.2f);
        this.tag = "EnemyTrapMinor";
    }

    private IEnumerator OnHitColor()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        meshRenderer.material.color = originColor;
    }
    public void BackHpMove()
    {
        backHpMove = true;
    }
}
