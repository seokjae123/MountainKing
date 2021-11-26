using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    public GameObject hpTarget;

    private void LateUpdate()
    {
        hpTarget.transform.LookAt(Camera.main.transform);
        hpTarget.transform.Rotate(0, 180, 0);
    }
}
