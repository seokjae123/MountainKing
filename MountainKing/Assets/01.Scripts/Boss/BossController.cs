using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    private BossAnimator bossAnimator;
    private BossMove bossMove;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    private BoxCollider boxCollider;

    public int Boss_Hp = 300;

    public Slider slider_Hp;
    public Slider slider_HpBack;
    public bool backHpMove = false;

    public GameObject fadeout;

    private void Awake()
    {
        bossAnimator = GetComponent<BossAnimator>();
        bossMove = GetComponent<BossMove>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        boxCollider = GetComponent<BoxCollider>();
    }
    public void Update()
    {
        slider_Hp.value = Boss_Hp;

        if (backHpMove)
        {
            slider_HpBack.value = Mathf.Lerp(slider_HpBack.value, slider_Hp.value, Time.deltaTime * 3f);
            if (slider_Hp.value >= slider_HpBack.value - 0.01f)
            {
                slider_HpBack.value = slider_Hp.value;
                backHpMove = false;
            }
        }

        if (Boss_Hp < 300 && Boss_Hp > 0)
        {
            if (backHpMove == false)
            {
                bossAnimator.animator.SetFloat("BossHp", Boss_Hp);
            }

        }
    }

    public void BossTakeDamage(int damage)
    {
        Debug.Log("작은 골렘" + damage + "만큼 감소");
        Boss_Hp -= damage;
        bossAnimator.OnHit();
        Invoke("BackHpMove", 0.5f);
        bossAnimator.Damaged(Boss_Hp);
        StartCoroutine("OnHitColor");
        StartCoroutine("OneHit");

        if (this.Boss_Hp <= 0)
        {
            boxCollider.enabled = false;
            bossMove.hpTarget.SetActive(false);
            bossMove.enabled = false;
            StartCoroutine("GameEnd");
        }
    }

    private IEnumerator OneHit()
    {
        this.tag = "Untagged";
        yield return new WaitForSeconds(0.2f);
        this.tag = "Boss";
    }

    private IEnumerator OnHitColor()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }

    private IEnumerator GameEnd()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("04.Ending");
    }
    public void BackHpMove()
    {
        backHpMove = true;
    }
}

