using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jaildooropen : MonoBehaviour
{
    public GameObject jailCol;
    public Image image;
    public Camera uicamera;

    void Start()
    {
        uicamera = Camera.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            image.gameObject.SetActive(true);

            //Debug.Log("감지 중");

            if (Input.GetKeyDown(KeyCode.G))
            {
                jailCol.SetActive(true);
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
            //Debug.Log("감지 끝");
        }
    }
}
