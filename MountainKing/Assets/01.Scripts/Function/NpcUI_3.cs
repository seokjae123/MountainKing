using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcUI_3 : MonoBehaviour
{
    public Image image;
    public Camera uicamera;
    private Transform target;
    public GameObject dialogue;
    public GameObject mark;

    private void Start()
    {
        target = GetComponent<Transform>();
        uicamera = Camera.FindObjectOfType<Camera>();
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
                this.gameObject.SetActive(false);
                image.gameObject.SetActive(false);
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
