using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate_5 : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableCamera;

    public Camera mainCamera;
    public Camera SearchingCamera;

    public GameObject dialogueAct;

    public void Start()
    {
        Player = GameObject.Find("Player");
        mainCamera = Camera.FindObjectOfType<Camera>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void PlayCamera() // 스위칭 카메라 이동
    {
        playableCamera.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 인정.");
            PlayCamera();
            StartCoroutine("CameraMove");
            //Destroy(this);
        }
    }

    private IEnumerator CameraMove() // 카메라 스위칭
    {
        mainCamera.enabled = false;
        SearchingCamera.enabled = true; // 카메라 스위칭
        playerMovement.enabled = false;
        yield return new WaitForSeconds(4f); // 전체 애니메이션 재생 시간
        mainCamera.enabled = true;
        SearchingCamera.enabled = false; // 카메라 스위칭
        playerMovement.enabled = true;
        this.gameObject.SetActive(false);
        dialogueAct.SetActive(true);
    }
}
