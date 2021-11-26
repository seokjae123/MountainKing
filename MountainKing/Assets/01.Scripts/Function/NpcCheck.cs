using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCheck : MonoBehaviour
{
    private EnemyController enemyController;
    public GameObject markObj;
    public GameObject dialogueObj;
    private bool UIon = false;

    void Start()
    {
        enemyController = GameObject.FindWithTag("ActionEnemy").GetComponent<EnemyController>();    
    }

    private void Update()
    {
        if (UIon == false && enemyController.Enemy_Hp <= 0)
        {
            markObj.SetActive(true);
            dialogueObj.SetActive(true);
            UIon = true;
        }
    }
}
