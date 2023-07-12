using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject normalPlayer;
    public GameObject bigPlayer;

    public AudioClip deathClip;         // 사망시 재생할 오디오 클립
    public float jumpForce = 400f;      // 점프 힘

    public int jumpCount = 0;           // 누적 점프 횟수
    private bool isGrounded = false;    // 바닥에 닿았는지 나타냄
    private bool isDead = false;        // 사망상태

    private Rigidbody2D playerRigidbody;    // 사용할 리지드 바디 컴포넌트
    private Animator animator;              // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio;        // 사용할 오디오 소스 컴포넌트

    public bool isBig = default;         // 버섯을 먹어서 커진 상태인가?
    public int life = 1;                 // 목숨

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        GFunc.Assert(playerRigidbody != null);
        GFunc.Assert(animator != null);
        GFunc.Assert(playerAudio != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }

        // LEGACY;
        //if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        //{
        //    // 점프 횟수 증가
        //    jumpCount++;

        //    // 점프 직전에 속도를 순간적으로 제로(0, 0)으로 변경
        //    playerRigidbody.velocity = Vector2.zero;

        //    // 리지드바디에 위쪽으로 힘주기
        //    // playerRigidbody.velocity = new Vector2(0, jumpForce);
        //    playerRigidbody.AddForce(new Vector2(0, jumpForce));

        //    // 오디오 소스 재생
        //    playerAudio.Play();
        //}
        //else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        //{
        //    // 마우스를 왼쪽버튼에서 손을 떼는 순간 && y속도의 값이 양수라면 (위로 상승 중)
        //    // 현재 속도를 절반으로 변경
        //    playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        //}

        //Jump();


        // 슈퍼점프
        if(Input.GetKeyDown(KeyCode.A))
        {
            // 리지드바디에 위쪽으로 힘주기
            // playerRigidbody.velocity = new Vector2(0, jumpForce);
            playerRigidbody.AddForce(new Vector2(0, jumpForce * 2));
        }

        // 슈퍼 낙하
        if (Input.GetMouseButtonUp(1))
        {
            // 중력을 높힘.
            playerRigidbody.gravityScale = 25f;
        }

        // 플레이어 위치 초기화
        if (transform.position.x < -6)
        {
            transform.position += new Vector3(0.01f, 0, 0);
            if (transform.position.x > -6)
            {
                transform.position -= new Vector3(0.01f, 0, 0);
            }
        }

        // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("isGround", isGrounded);
    }

    // 모바일에서 버튼을 눌러 플레이어를 점프 시키게 하기 위한 함수
    public void Jump()
    {
        if (jumpCount < 2)
        {
            // 점프 횟수 증가
            jumpCount++;

            // 점프 직전에 속도를 순간적으로 제로(0, 0)으로 변경
            playerRigidbody.velocity = Vector2.zero;

            // 리지드바디에 위쪽으로 힘주기
            // playerRigidbody.velocity = new Vector2(0, jumpForce);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            // 오디오 소스 재생
            playerAudio.Play();
        }
        //else if (playerRigidbody.velocity.y > 0)
        //{
        //    // 마우스를 왼쪽버튼에서 손을 떼는 순간 && y속도의 값이 양수라면 (위로 상승 중)
        //    // 현재 속도를 절반으로 변경
        //    playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        //}
    }

    // 모바일 용 슈퍼점프
    public void SuperJump()
    {
        // 리지드바디에 위쪽으로 힘주기
        // playerRigidbody.velocity = new Vector2(0, jumpForce);
        playerRigidbody.AddForce(new Vector2(0, jumpForce * 2));
    }

    // 모바일 용 착지
    public void Land()
    {
        // 중력을 높힘.
        playerRigidbody.gravityScale = 25f;
    }

    private void Die()
    {
        // 애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("isDie");

        // 오디오 소스에 할당된 오디오 클립을 deathClip으로 변경
        playerAudio.clip = deathClip;
        // 사망 효과음 재생
        playerAudio.Play();

        // 속도를 제로(0, 0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        // 게임 매니저의 게임오버 처리 실행
        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 데드존을 밟았을 때,
        if (collision.tag.Equals("Dead") && isDead == false)
        {
            // 라이프가 2라면
            if (life == 2)
            {
                // 충돌한 것 지우고,
                collision.gameObject.SetActive(false);
                // 큰 플레이어 프리팹도 지웁니다.
                bigPlayer.SetActive(false);
                // 작은 플레이어 프리팹을 생성합니다.
                //PlayerController normal = Instantiate(normalPlayer, transform.position, Quaternion.identity).GetComponent<PlayerController>();
                //normal.isBig = false;
            }

            // 라이프를 1로 만듭니다.
            life--;

            // 라이프가 0이 되면
            if (life <= 0)
            {
                // 죽습니다.
                Die();
            }
        }

        // 버섯을 먹었을 때,
        // 크기를 프리팹으로 바꾸니까 오류 생겨서 일단 지워놓음.
        //if (collision.tag.Equals("Mushroom") && (life < 2))
        //{
        //    bigPlayer.SetActive(true);
        //    normalPlayer.SetActive(false);

        //    // PlayerController big = Instantiate(bigPlayer, transform.position, Quaternion.identity).GetComponent<PlayerController>();
        //    //big.isBig = true;
        //    //big.life = 2;
        //}

        // 깃털을 먹었을 때,
        if (collision.tag.Equals("Feather"))
        {
            jumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (0.7f < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
            playerRigidbody.gravityScale = 1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

}
