using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ������ �Ծ��� ��, 
        if (collision.tag == "Player")
        {
            GFunc.Log("���п� ��Ҵ�. ����ī��Ʈ �ʱ�ȭ");
            gameObject.SetActive(false);

            // ���� ī��Ʈ ����
            playerController.jumpCount = 0;
        }
    }
}
