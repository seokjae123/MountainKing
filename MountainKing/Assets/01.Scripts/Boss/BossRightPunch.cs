using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRightPunch : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PlayerTakeDamage(15);
        }
    }

    private IEnumerator AutoDisable()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
