using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torch_2 : MonoBehaviour
{
    public bool isFire = false;
    public GameObject fire;
    public Image image;
    public Camera uicamera;

    void Start()
    {
        uicamera = Camera.FindObjectOfType<Camera>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire == true)
        {
            fire.SetActive(true);
        }

        else if (isFire == false)
        {
            fire.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            image.gameObject.SetActive(true);

            //Debug.Log("감지 중");

            if (Input.GetKeyDown(KeyCode.G))
            {
                if (isFire == true)
                {
                    isFire = false;
                }

                else if (isFire == false)
                {
                    isFire = true;
                }
            }
        }
    }

    //NPC 대화 범위 밖으로 나감
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            image.gameObject.SetActive(false);
            Debug.Log("감지 끝");
        }
    }
}
