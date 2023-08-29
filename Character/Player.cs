using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player의 점프 변수
    [SerializeField] float jumpForce = 0f;
    [SerializeField] int maxJumpCount = 0;
    public int jumpCount = 0;
    public Button JumpButton;
    public GameObject monster;
    bool isJump = false;
    //2번 이상 점프를 위한 변수
    float distance = 0;
    [SerializeField] LayerMask layerMask = 0;

    //생명&오버 구현
    public int health;
    public Image[] UIHealth;
    public Image[] finalUI;
    public GameObject retryBtn;

    private GameManager gameManager;

    //public AudioClip clip;

    //무적
    SpriteRenderer player;
    Rigidbody2D rigid;

    Vector2 startPosition;

    //애니메이션
    Animator anim;

    void Start()
    {
        startPosition = transform.position;  //시작위치를 startPosition에 초기화
        anim = GetComponent<Animator>();
        player = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        distance = GetComponent<CircleCollider2D>().bounds.extents.y + 0.05f;
    }

    public void TryJump()
    {
        //지금 현재의 문제점은 클릭이 됐을때 한번에 JumpCount가 maxJumpCount가 됨
        JumpButton.onClick.AddListener(() =>
        {
            if (jumpCount < maxJumpCount && !isJump)
            {
                isJump = true;
                //Sound.instance.SFXPlay("Jump", clip);
                jumpCount += 1;
                rigid.velocity = Vector2.up * jumpForce;
            }
        });
        isJump = false;
    }
    
    //발 밑에 땅이 있는지 확인하는 함수(점프초기화)
    public void CheckGround()
    {
        //if (rigid.velocity.y < 0)
        //{
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask);
            if (hit)
            {              
                if (hit.transform.CompareTag("Ground") || hit.transform.CompareTag("Mob"))
                {
                    isJump = false;
                    jumpCount = 0;
                }
            }
        //}
    }

    void Update()
    {
            TryJump();
            CheckGround();
            anim.SetBool("roll", true);
    }

    //충돌했을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //몬스터 + 1R일때
        if (collision.CompareTag("Mob") && SceneManager.GetActiveScene().buildIndex==1)
        {
            if (health > 1)
            {
                OnDamaged();
                UIHealth[health].color = new Color(1, 0, 0, 0.4f);
                finalUI[health].color = new Color(1, 1, 1, 0.5f);
            }               
            else
            {
                UIHealth[0].color = new Color(1, 0, 0, 0.4f);
                finalUI[0].color = new Color(1, 1, 1, 0.5f);
                OnDie();
                
                Invoke("BtnCreate", 0.01f);
            }
            collision.gameObject.SetActive(false);
        }
        //몬스터 + 2R
        if (collision.CompareTag("Mob") && SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (health == 3)
            {
                OnDamaged2();
                this.gameObject.transform.position = new Vector3(-0.5f, -1.89f, 0);
            }
            else if (health == 2)
            {
                OnDamaged2();
                this.gameObject.transform.position = new Vector3(-1.0f, -1.89f, 0);
            }
            else if (health == 1)
            {
                OnDamaged2();
                this.gameObject.transform.position = new Vector3(-1.5f, -1.89f, 0);
            }
            else
            {
                this.gameObject.transform.position = new Vector3(-1.54f, 1.89f, 0);
                //transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(-0.2f, 0.6f, 0), Time.deltaTime * 2);
                OnDie();
                Invoke("BtnCreate", 0.05f);
            }
            collision.gameObject.SetActive(false);
        }
        //아이템
        if (collision.CompareTag("Item"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (health == 3)
                {
                    GameManager.instance.score += 50;
                }
                else
                {
                    health += 1;
                    UIHealth[health - 1].color = new Color(1, 1, 1, 1);
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                ItemEff();
                //GameManager.instance.item2.SetActive(false);
            }
        }
        //문이랑
        if (collision.CompareTag("Door"))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(3);
        }
    }

    //2R 아이템 효과(무적)
    void ItemEff()
    {
        player.transform.localScale = new Vector3(2, 2, 2);
        player.color = new Color(1, 0.714f, 0.996f);
        gameObject.layer = 12;

        Invoke("OriginSize", 5f);
    }

    //1R 데미지
    void OnDamaged()
    {
        health--;
        //무적모드 레이어 설정
        gameObject.layer = 11;
        //불투명
        player.color = new Color(1, 1, 1, 0.5f);

        Invoke("OffDamaged", 0.5f);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        player.color = new Color(1, 1, 1, 1);
        //player.transform.localScale = new Vector3(1, 1, 1);
    }

    //2R데미지
    void OnDamaged2()
    {
        health--;
        //무적모드 레이어 설정
        gameObject.layer = 11;
        //불투명
        player.color = new Color(0.55f, 0.52f, 0.67f, 0.5f);

        Invoke("OffDamaged2", 0.7f);
    }

    void OffDamaged2()
    {
        gameObject.layer = 10;
        player.color = new Color(0.55f, 0.52f, 0.67f, 1);
        //player.transform.localScale = new Vector3(1, 1, 1);
    }

    void OriginSize()
    {
        player.transform.localScale = new Vector3(1, 1, 1);

        Invoke("OffDamaged2", 0.7f);
    }

    void OnDie()
    {
        anim.SetTrigger("doDie");
        //player.color = new Color(1, 1, 1, 0.5f);
        //player.flipY = true;

        GameManager.instance.isPlay = false;
        GameManager.instance.ForJump.SetActive(false);
    }

    void BtnCreate()
    {
        Time.timeScale = 0;
        retryBtn.SetActive(true);
    }
}