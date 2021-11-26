using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcUI_2 : MonoBehaviour
{
    public Image image;
    public Camera uicamera;
    private Transform target;
    public GameObject dialogue;
    public GameObject mark;
    public GameObject wall;
    public GameObject linked_Dialogue;

    private void Start()
    {
        target = GetComponent<Transform>();
    }

    private void Update()
    {
        //Vector3 screenPos = uicamera.WorldToScreenPoint(target.position);
        //float x = screenPos.x;
        //image.transform.position = new Vector3(x, screenPos.y * 1.2f, image.transform.position.z);
    }
    //NPC 대화 범위 안에 있음
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            image.gameObject.SetActive(true);
            Debug.Log("감지 중");

            if (Input.GetKeyDown(KeyCode.G))
            {
                dialogue.SetActive(true);
                mark.SetActive(false);
                wall.SetActive(false);
                this.gameObject.SetActive(false);
                image.gameObject.SetActive(false);

                if (linked_Dialogue.activeSelf == true)
                {
                    linked_Dialogue.SetActive(false);
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
