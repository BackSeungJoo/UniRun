using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ��1�� �Ծ��� ��,
        if (collision.tag == "Player")
        {
            GFunc.Log("�� 1�� ��Ҵ�! ���� 3��");
            gameObject.SetActive(false);

            // ���� 3 ����
            GameManager.instance.AddScore(3);
        }
    }
}
