using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActionEnemy"))
        {
            other.GetComponent<EnemyController>().EnemyTakeDamage(35);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().EnemyTakeDamage(35);
        }

        if (other.CompareTag("EnemyMinor"))
        {
            other.GetComponent<EnemyMinorController>().EnemyTakeDamage(35);
        }

        if (other.CompareTag("EnemyTrapMinor"))
        {
            other.GetComponent<EnemyTrapMinorController>().EnemyTakeDamage(35);
        }

        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossController>().BossTakeDamage(35);
        }

        if (other.CompareTag("Rock"))
        {
            other.GetComponent<RockController>().HitRock(1);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }
}
