using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate_6 : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableCamera;

    public Camera mainCamera;
    public Camera jailDoorCamera;

    public GameObject dialogueAct;

    private BoxCollider jaildoorCol;

    public void Start()
    {
        Player = GameObject.Find("Player");
        mainCamera = Camera.FindObjectOfType<Camera>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        jaildoorCol = GameObject.Find("JailDoor").GetComponent<BoxCollider>();
    }

    public void PlayJailDoor() // 스위칭 카메라 이동
    {
        playableDirector.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 인정.");
            PlayJailDoor();
            StartCoroutine("CameraMove");
            //Destroy(this);
        }
    }

    private IEnumerator CameraMove() // 카메라 스위칭
    {
        //mainCamera.enabled = false;
        //jailDoorCamera.enabled = true; // 카메라 스위칭
        playerMovement.enabled = false;
        jaildoorCol.enabled = false;
        yield return new WaitForSeconds(2f); // 전체 애니메이션 재생 시간
        //mainCamera.enabled = true;
        //jailDoorCamera.enabled = false; // 카메라 스위칭
        playerMovement.enabled = true;
        jaildoorCol.enabled = true;
        jaildoorCol.isTrigger = false;
        this.gameObject.SetActive(false);
        dialogueAct.SetActive(true);
    }
}
