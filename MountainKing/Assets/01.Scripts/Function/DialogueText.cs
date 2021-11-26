using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
}

public class DialogueText : MonoBehaviour
{
    [SerializeField]
    private Text txt;
    [SerializeField]
    private Image img;

    private bool isDialogue = false;
    private int txt_count = 0;
    private int img_count = 0;

    [SerializeField]
    private Dialogue[] dialogue;
    [SerializeField]
    public Image[] image;

    PlayerController playerController;
    PlayerAnimator playerAnimator;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAnimator = GameObject.Find("Character").GetComponent<PlayerAnimator>();
    }
    //대화창 시작
    public void ShowDialogue()
    {
        img.gameObject.SetActive(true);
        txt.gameObject.SetActive(true);
        txt_count = 0;
        img_count = 0;
        isDialogue = true;
        NextDialogue();
    }
    //대화창 넘김
    private void NextDialogue()
    {
        txt.text = dialogue[txt_count].dialogue;
        image[img_count].gameObject.SetActive(true);
        txt_count++;
    }
    //대화창 닫음
    private void CloseDialogue()
    {
        img.gameObject.SetActive(false);
        txt.gameObject.SetActive(false);
        image[img_count].gameObject.SetActive(false);

        isDialogue = false;
        playerController.isDialogue = false;
        playerController.GetComponent<PlayerMovement>().enabled = true;
        this.gameObject.SetActive(false);
    }
    //대화창 시작 후 g키 입력으로 대화창 넘김
    void FixedUpdate()
    {
        if (isDialogue == true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                //Debug.Log("g 입력받음.");
                if (txt_count < dialogue.Length)
                {
                    image[img_count].gameObject.SetActive(false);
                    img_count++;
                    NextDialogue();
                }
                else
                    CloseDialogue();
            }
        }
    }

    //플레이어가 대화시작 콜라이더에 닿으면 대화 시작 + 플레이어 움직임 멈춤
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowDialogue();
            isDialogue = true;
            playerController.isDialogue = true;
            playerAnimator.OnMovement(0, 0);
            playerController.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("탈출");
        }
    }
}
