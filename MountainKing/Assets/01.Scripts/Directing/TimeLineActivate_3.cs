using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate_3 : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;

    public Camera mainCamera;
    public Camera BossCamera;

    public AudioSource bossEncounter;
    public AudioSource bgm;
    public void Start()
    {
        Player = GameObject.Find("Player");
        mainCamera = Camera.FindObjectOfType<Camera>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void PlayBoss() // 대상 애니메이션 작동
    {
        playableDirector.Play();
    }

    public void PlayCamera() // 스위칭 카메라 이동
    {
        playableDirector2.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 인정.");
            PlayCamera();
            PlayBoss();
            bgm.mute = true;
            bossEncounter.Play();
            //StartCoroutine("Directing");
            StartCoroutine("CameraMove");
            //Destroy(this);
        }
    }

    //private IEnumerator Directing() // 애니메이션 재생
    //{
    //    PlayCamera();
    //    //yield return new WaitForSeconds(1.3f);
    //    PlayBoss();        
    //}

    private IEnumerator CameraMove() // 카메라 스위칭
    {
        mainCamera.enabled = false;
        BossCamera.enabled = true; // 카메라 스위칭
        playerMovement.enabled = false;
        yield return new WaitForSeconds(8f); // 전체 애니메이션 재생 시간
        mainCamera.enabled = true;
        BossCamera.enabled = false; // 카메라 스위칭
        playerMovement.enabled = true;
        this.gameObject.SetActive(false);
    }
}
