using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate_7 : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;

    public Camera mainCamera;
    public Camera enemyRunCamera;

    public GameObject dialogueAct;

    public void Start()
    {
        Player = GameObject.Find("Player");
        mainCamera = Camera.FindObjectOfType<Camera>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void PlayCamera() // 스위칭 카메라 이동
    {
        //playableDirector2.Play();
    }
    public void EnemyRun() // 스위칭 카메라 이동
    {
        playableDirector.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("마이너 런 충돌 인정.");
            PlayCamera();
            EnemyRun();
            StartCoroutine("CameraMove");
            //Destroy(this);
        }
    }

    private IEnumerator CameraMove() // 카메라 스위칭
    {
        mainCamera.enabled = false;
        enemyRunCamera.enabled = true; // 카메라 스위칭
        playerMovement.enabled = false;
        yield return new WaitForSeconds(3.5f); // 전체 애니메이션 재생 시간
        mainCamera.enabled = true;
        enemyRunCamera.enabled = false; // 카메라 스위칭
        playerMovement.enabled = true;
        playableDirector.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        dialogueAct.SetActive(true);
    }
}
