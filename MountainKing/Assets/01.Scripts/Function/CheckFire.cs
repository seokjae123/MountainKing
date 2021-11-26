using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFire : MonoBehaviour
{
    public GameObject target;
    private SphereCollider torch_1;
    private SphereCollider torch_2;
    private Torch_1 torch1;
    private Torch_2 torch2;

    void Start()
    {
        torch_1 = GameObject.Find("SphereCol1").GetComponent<SphereCollider>();
        torch_2 = GameObject.Find("SphereCol2").GetComponent<SphereCollider>();
        torch1 = GameObject.Find("SphereCol1").GetComponent<Torch_1>();
        torch2 = GameObject.Find("SphereCol2").GetComponent<Torch_2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (torch1.isFire == true && torch2.isFire == true)
        {
            target.SetActive(true);
            torch_1.enabled = false;
            torch_2.enabled = false;
        }
    }
}
