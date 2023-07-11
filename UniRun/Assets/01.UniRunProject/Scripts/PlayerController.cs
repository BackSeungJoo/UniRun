using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;         // ����� ����� ����� Ŭ��
    public float jumpForce = 700f;      // ���� ��

    public int jumpCount = 0;          // ���� ���� Ƚ��
    private bool isGrounded = false;    //�ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false;        // �������

    private Rigidbody2D playerRigidbody;    // ����� ������ �ٵ� ������Ʈ
    private Animator animator;              // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio;        // ����� ����� �ҽ� ������Ʈ

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

        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // ���� Ƚ�� ����
            jumpCount++;

            // ���� ������ �ӵ��� ���������� ����(0, 0)���� ����
            playerRigidbody.velocity = Vector2.zero;

            // ������ٵ� �������� ���ֱ�
            // playerRigidbody.velocity = new Vector2(0, jumpForce);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            // ����� �ҽ� ���
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // ���콺�� ���ʹ�ư���� ���� ���� ���� && y�ӵ��� ���� ������ (���� ��� ��)
            // ���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // �ִϸ������� Grounded �Ķ���͸� isGrounded ������ ����
        animator.SetBool("isGround", isGrounded);
    }

    private void Die()
    {
        // �ִϸ������� Die Ʈ���� �Ķ���͸� ��
        animator.SetTrigger("isDie");

        // ����� �ҽ��� �Ҵ�� ����� Ŭ���� deathClip���� ����
        playerAudio.clip = deathClip;
        // ��� ȿ���� ���
        playerAudio.Play();

        // �ӵ��� ����(0, 0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;

        // ���� �Ŵ����� ���ӿ��� ó�� ����
        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �������� ����� ��,
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
