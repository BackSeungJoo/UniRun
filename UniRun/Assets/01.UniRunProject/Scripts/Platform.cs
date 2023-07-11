using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������μ� �ʿ��� ������ ���� ��ũ��Ʈ
public class Platform : MonoBehaviour
{
    public GameObject[] items;          // ������ ������Ʈ
    public GameObject[] obstacles;      // ��ֹ� ������Ʈ
    private bool stepped = false;       // �÷��̾� ĳ���Ͱ� ��Ҵ°�?

    private int itemYes = default;  // ������ ���� ���� �Ǵ�
    private int whatItem0 = default; // � ������ ������ ����
    private int whatItem1 = default; // � ������ ������ ����
    private int whatItem2 = default; // � ������ ������ ����
    private int whatItem3 = default; // � ������ ������ ����

    // ������Ʈ�� Ȱ��ȭ�� ������ �Ź� ����Ǵ� �޼���
    private void OnEnable()
    {
        // ���� ���¸� ����
        stepped = false;

        // ��ֹ��� ����ŭ ����
        for (int i = 0; i < obstacles.Length; i++)
        {
            // ���� ������ ��ֹ��� 1/5�� Ȯ���� Ȱ��ȭ
            if(Random.Range(0, 5) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }

        // ���� ������ ����
        // �������� �����Ұ��� �ƴ��� ���� üũ
        itemYes = Random.Range(0, 7);

        // 1�̻��̸� �������� �����մϴ�.
        if (itemYes > 1)
        {
            // �����ߴٸ� � �������� ���� �Ұ��� üũ 
            whatItem0 = Random.Range(0, 3);
            whatItem1 = Random.Range(0, 2);
            whatItem2 = Random.Range(0, 2);
            whatItem3 = Random.Range(0, 8);
            
            // 0�� ������, ����
            if(whatItem0 == 0)
            {
                items[0].SetActive(true);
            }
            else
            {
                items[0].SetActive(false);
            }
            // 1�� ������, ��1
            if (whatItem1 == 0)
            {
                items[1].SetActive(true);
            }
            else
            {
                items[1].SetActive(false);
            }

            // 2�� ������, ��2
            if (whatItem2 == 0)
            {
                items[2].SetActive(true);
            }
            else
            {
                items[2].SetActive(false);
            }

            // 3�� ������ Ŭ�ι�
            if (whatItem3 == 0)
            {
                items[3].SetActive(true);
            }
            else
            {
                items[3].SetActive(false);
            }
        }

        // �ƴ϶�� ����
        else
        {
            return;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������ �±װ� Player�̰� ������ �÷��̾� ĳ���Ͱ� ���� �ʾҴٸ�
        if((collision.collider.tag == "Player") && (stepped == false))
        {
            // ������ �߰��ϰ� ���� ���¸� ������ ����
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
