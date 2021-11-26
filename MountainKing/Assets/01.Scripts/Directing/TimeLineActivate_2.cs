using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineActivate_2 : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;

    public Camera mainCamera;
    public Camera helpCamera;

    public void PlayHelp()
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
            //Destroy(this);
        }
    }

    private IEnumerator Directing()
    {
        PlayCamera();
        yield return new WaitForSeconds(2.0f);
        PlayHelp();        
    }

    private IEnumerator CameraMove()
    {
        mainCamera.enabled = false;
        helpCamera.enabled = true;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(4.0f);
        mainCamera.enabled = true;
        helpCamera.enabled = false;
        playerMovement.enabled = true;
        this.gameObject.SetActive(false);
    }
}
