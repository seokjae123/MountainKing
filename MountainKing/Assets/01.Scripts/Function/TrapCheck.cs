using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCheck : MonoBehaviour
{
    public int trap_enemy_die = 0;
    public GameObject trap_dialogue;
    public GameObject questMark;
    private bool trap_act = false;

    // Update is called once per frame
    void Update()
    {
        if (trap_enemy_die >= 8 && trap_act == false)
        {
            trap_dialogue.SetActive(true);
            questMark.SetActive(true);
            trap_act = true;
        }
    }
}
