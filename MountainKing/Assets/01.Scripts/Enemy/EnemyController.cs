using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private EnemyMove enemymove;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    private BoxCollider boxCollider;
    public float Enemy_Hp = 100;
    public GameObject questmark;
    public GameObject dialogue;
    public GameObject portal = null;

    public Slider slider_Hp;
    public Slider slider_HpBack;
    public bool backHpMove = false;

    public AudioSource enemyDead;
    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemymove = GetComponent<EnemyMove>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        boxCollider = GetComponent<BoxCollider>();
    }

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
                enemyAnimator.animator.SetFloat("EnemyHp", Enemy_Hp);
            }

        }
    }

    public void EnemyTakeDamage(int damage)
    {
        Debug.Log("작은 골렘" + damage + "만큼 감소");
        enemyAnimator.OnHit();
        Enemy_Hp -= damage;
        Invoke("BackHpMove", 0.5f);
        enemyAnimator.Damaged(Enemy_Hp);
        StartCoroutine("OnHitColor");
        StartCoroutine("OneHit");

        if (this.Enemy_Hp <= 0)
        {
            enemyDead.Play();
            boxCollider.enabled = false;
            enemymove.hpTarget.SetActive(false);
            enemymove.enabled = false;
            portal.SetActive(true);
        }
    }

    private IEnumerator OneHit()
    {
        this.tag = "Untagged";
        yield return new WaitForSeconds(0.2f);
        this.tag = "Enemy";
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
