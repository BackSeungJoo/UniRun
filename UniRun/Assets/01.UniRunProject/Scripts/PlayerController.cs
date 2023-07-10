using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;         // 사망시 재생할 오디오 클립
    public float jumpForce = 700f;      // 점프 힘

    private int jumpCount = 0;          // 누적 점프 횟수
    private bool isGrounded = false;    //바닥에 닿았는지 나타냄
    private bool isDead = false;        // 사망상태

    private Rigidbody2D playerRigidbody;    // 사용할 리지드 바디 컴포넌트
    private Animator animator;              // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio;        // 사용할 오디오 소스 컴포넌트

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

        if (Input.GetMouseButtonDown(0) /*&& jumpCount < 2*/)
        {
            // 점프 횟수 증가
            jumpCount++;

            // 점프 직전에 속도를 순간적으로 제로(0, 0)으로 변경
            playerRigidbody.velocity = Vector2.zero;

            // 리지드바디에 위쪽으로 힘주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            // 오디오 소스 재생
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스를 왼쪽버튼에서 손을 떼는 순간 && y속도의 값이 양수라면 (위로 상승 중)
            // 현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("isGround", isGrounded);
    }

    private void Die()
    {
        animator.SetTrigger("isDie");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Dead") && isDead == false)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (0.7f < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

}
