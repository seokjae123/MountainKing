using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;

    public Camera mainCamera;
    public Camera rockCamera;

    public void PlayRock()
    {
        playableDirector.Play();
    }

    public void PlayCamera()
    {
        playableDirector2.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 인정.");
            StartCoroutine("Directing");
            StartCoroutine("CameraMove");
        }
    }

    private IEnumerator Directing()
    {
        PlayCamera();
        yield return new WaitForSeconds(1.0f);
        PlayRock();        
    }

    private IEnumerator CameraMove()
    {
        mainCamera.enabled = false;
        rockCamera.enabled = true;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(3.0f);
        mainCamera.enabled = true;
        rockCamera.enabled = false;
        playerMovement.enabled = true;
        this.gameObject.SetActive(false);
    }
}
