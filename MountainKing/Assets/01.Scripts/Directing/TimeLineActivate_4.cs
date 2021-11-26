using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate_4 : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;

    public Camera mainCamera;
    public Camera RockRollingCamera;

    private Torch_1 torch1;
    private Torch_2 torch2;

    public void Start()
    {
        Player = GameObject.Find("Player");
        mainCamera = Camera.FindObjectOfType<Camera>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        torch1 = GameObject.Find("SphereCol1").GetComponent<Torch_1>();
        torch2 = GameObject.Find("SphereCol2").GetComponent<Torch_2>();
    }

    public void PlayRockRolling() // 대상 애니메이션 작동
    {
        playableDirector.Play();
    }

    public void PlayCamera() // 스위칭 카메라 이동
    {
        //playableDirector2.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 인정.");
            PlayCamera();
            PlayRockRolling();
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
        torch1.isFire = false;
        torch2.isFire = false;
        torch1.image.gameObject.SetActive(false);
        torch2.image.gameObject.SetActive(false);

        mainCamera.enabled = false;
        RockRollingCamera.enabled = true; // 카메라 스위칭
        playerMovement.enabled = false;
        Debug.Log("스위칭 됨");
        yield return new WaitForSeconds(4f); // 전체 애니메이션 재생 시간
        Debug.Log("영상 재생 완료");
        mainCamera.enabled = true;
        RockRollingCamera.enabled = false; // 카메라 스위칭
        playerMovement.enabled = true;
        Debug.Log("다시 스위칭 됨");
        this.gameObject.SetActive(false);      
        Debug.Log("오브젝트 사라짐");
    }
}
