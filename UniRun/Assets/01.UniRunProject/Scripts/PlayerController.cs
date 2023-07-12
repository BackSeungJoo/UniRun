using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject normalPlayer;
    public GameObject bigPlayer;

    public AudioClip deathClip;         // ����� ����� ����� Ŭ��
    public float jumpForce = 400f;      // ���� ��

    public int jumpCount = 0;           // ���� ���� Ƚ��
    private bool isGrounded = false;    // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false;        // �������

    private Rigidbody2D playerRigidbody;    // ����� ������ �ٵ� ������Ʈ
    private Animator animator;              // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio;        // ����� ����� �ҽ� ������Ʈ

    public bool isBig = default;         // ������ �Ծ Ŀ�� �����ΰ�?
    public int life = 1;                 // ���

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
        //    // ���� Ƚ�� ����
        //    jumpCount++;

        //    // ���� ������ �ӵ��� ���������� ����(0, 0)���� ����
        //    playerRigidbody.velocity = Vector2.zero;

        //    // ������ٵ� �������� ���ֱ�
        //    // playerRigidbody.velocity = new Vector2(0, jumpForce);
        //    playerRigidbody.AddForce(new Vector2(0, jumpForce));

        //    // ����� �ҽ� ���
        //    playerAudio.Play();
        //}
        //else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        //{
        //    // ���콺�� ���ʹ�ư���� ���� ���� ���� && y�ӵ��� ���� ������ (���� ��� ��)
        //    // ���� �ӵ��� �������� ����
        //    playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        //}

        //Jump();


        // ��������
        if(Input.GetKeyDown(KeyCode.A))
        {
            // ������ٵ� �������� ���ֱ�
            // playerRigidbody.velocity = new Vector2(0, jumpForce);
            playerRigidbody.AddForce(new Vector2(0, jumpForce * 2));
        }

        // ���� ����
        if (Input.GetMouseButtonUp(1))
        {
            // �߷��� ����.
            playerRigidbody.gravityScale = 25f;
        }

        // �÷��̾� ��ġ �ʱ�ȭ
        if (transform.position.x < -6)
        {
            transform.position += new Vector3(0.01f, 0, 0);
            if (transform.position.x > -6)
            {
                transform.position -= new Vector3(0.01f, 0, 0);
            }
        }

        // �ִϸ������� Grounded �Ķ���͸� isGrounded ������ ����
        animator.SetBool("isGround", isGrounded);
    }

    // ����Ͽ��� ��ư�� ���� �÷��̾ ���� ��Ű�� �ϱ� ���� �Լ�
    public void Jump()
    {
        if (jumpCount < 2)
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
        //else if (playerRigidbody.velocity.y > 0)
        //{
        //    // ���콺�� ���ʹ�ư���� ���� ���� ���� && y�ӵ��� ���� ������ (���� ��� ��)
        //    // ���� �ӵ��� �������� ����
        //    playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        //}
    }

    // ����� �� ��������
    public void SuperJump()
    {
        // ������ٵ� �������� ���ֱ�
        // playerRigidbody.velocity = new Vector2(0, jumpForce);
        playerRigidbody.AddForce(new Vector2(0, jumpForce * 2));
    }

    // ����� �� ����
    public void Land()
    {
        // �߷��� ����.
        playerRigidbody.gravityScale = 25f;
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
            // �������� 2���
            if (life == 2)
            {
                // �浹�� �� �����,
                collision.gameObject.SetActive(false);
                // ū �÷��̾� �����յ� ����ϴ�.
                bigPlayer.SetActive(false);
                // ���� �÷��̾� �������� �����մϴ�.
                //PlayerController normal = Instantiate(normalPlayer, transform.position, Quaternion.identity).GetComponent<PlayerController>();
                //normal.isBig = false;
            }

            // �������� 1�� ����ϴ�.
            life--;

            // �������� 0�� �Ǹ�
            if (life <= 0)
            {
                // �׽��ϴ�.
                Die();
            }
        }

        // ������ �Ծ��� ��,
        // ũ�⸦ ���������� �ٲٴϱ� ���� ���ܼ� �ϴ� ��������.
        //if (collision.tag.Equals("Mushroom") && (life < 2))
        //{
        //    bigPlayer.SetActive(true);
        //    normalPlayer.SetActive(false);

        //    // PlayerController big = Instantiate(bigPlayer, transform.position, Quaternion.identity).GetComponent<PlayerController>();
        //    //big.isBig = true;
        //    //big.life = 2;
        //}

        // ������ �Ծ��� ��,
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
