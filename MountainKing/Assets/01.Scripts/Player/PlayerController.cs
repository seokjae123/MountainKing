using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    private PlayerMovement playerMovement;
    private PlayerAnimator playerAnimator;
    private CapsuleCollider capsuleCollider;
    private GameObject player;
    public float Player_Hp = 100;
    public float maxPlayer_Hp = 100;
    public bool IsDead = false;
    public bool isDialogue;
    private TimeLineController timeLineController;

    public Slider slider_Hp;
    public Slider slider_HpBack;
    public bool backHpMove = false;

    public GameObject resurrectMessage;

    public AudioSource playerDead;
    public AudioSource playerHit;

    void Awake()
    {
        Cursor.visible = false;                     // 마우스 커서 없애기
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서 위치 고정

        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        originColor = meshRenderer.material.color;
        timeLineController = GetComponent<TimeLineController>();
        player = GameObject.Find("Player");
        isDialogue = false;
    }

    public void Update()
    {
        slider_Hp.value = Player_Hp;
        
        if (backHpMove)
        {
            slider_HpBack.value = Mathf.Lerp(slider_HpBack.value, slider_Hp.value, Time.deltaTime * 5f);
            if (slider_Hp.value >= slider_HpBack.value - 0.01f)
            {
                slider_HpBack.value = slider_Hp.value;
                backHpMove = false;
            }
        }

        else if (Player_Hp < 100 && Player_Hp > 0)
        {
            if (backHpMove == false)
            {
                Player_Hp += 3 * Time.deltaTime;
                slider_HpBack.value += 3 * Time.deltaTime;
                playerAnimator.animator.SetFloat("Player_HP", Player_Hp);
            }
            
        }

        if (IsDead)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("우클릭 입력받음");
                StartCoroutine("Resurrect");
            }
        }

        //대화 중이 아닐 때 움직임
        else if (isDialogue == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            playerAnimator.OnMovement(x, z);

            // 이동할 때 속도 5
            playerMovement.speed = 5.0f;

            if (Input.GetMouseButtonDown(0))
            {
                playerAnimator.IsAttack();
                playerMovement.speed = 0.0f;
            }
        }

        // 회전 설정 ( 캐릭터 회전을 카메라와 같은 회전값으로 ) 이거 건들면 캐릭터 굳어서 죽음
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    //데미지 입으면 처리 (애니+체력차감)
    public void PlayerTakeDamage(int damage)
    {
        Debug.Log("플레이어" + damage + "만큼 체력 감소.");
        playerAnimator.IsHit();
        playerHit.Play();
        Player_Hp -= damage;
        Invoke("BackHpMove", 0.5f);
        playerAnimator.Damaged(Player_Hp);
        StartCoroutine("OnHitColor");
        StartCoroutine("OneHit");

        if (Player_Hp < 0)
        {
            playerMovement.enabled = false;
            player.gameObject.tag = "Untagged";
            IsDead = true;
            playerDead.Play();
            resurrectMessage.SetActive(true);
        }
    }  
    // 맞을 때 태그 변경 > 연속으로 쳐맞기 금지
    private IEnumerator OneHit()
    {
        this.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(0.5f);
        this.gameObject.tag = "Player";
    }
    //부활
    private IEnumerator Resurrect()
    {
        playerMovement.enabled = true;
        Player_Hp = maxPlayer_Hp;
        playerAnimator.animator.SetBool("isResurrect", true);
        playerAnimator.Resurrect(maxPlayer_Hp);
        resurrectMessage.SetActive(false);
        IsDead = false;
        yield return new WaitForSeconds(0.5f);
        player.gameObject.tag = "Player";
    }
    // 맞을 때 붉어지는 효과
    private IEnumerator OnHitColor()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material.color = originColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            Debug.Log("닿았다.");
            SceneManager.LoadScene("03.Stage_2");
            Scene scene = SceneManager.GetSceneByBuildIndex(1);
            SceneManager.MoveGameObjectToScene(this.gameObject, scene);
        }

        if (other.CompareTag("Portal2S"))
        {
            Debug.Log("닿았다.");
            this.transform.position = GameObject.FindWithTag("Portal2E").transform.position;
        }

    }
    
    public void BackHpMove ()
    {
        backHpMove = true;
    }
}
