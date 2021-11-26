using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeftPunch : MonoBehaviour
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
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}
