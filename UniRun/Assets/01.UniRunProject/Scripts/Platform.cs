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
    private int whatItem = default; // � ������ ������ ����

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
        items[0].SetActive(false);
        items[1].SetActive(false);
        items[2].SetActive(false);

        // �������� �����Ұ��� �ƴ��� ���� üũ
        itemYes = Random.Range(0, 5);

        // 1�̻��̸� �������� �����մϴ�.
        if (itemYes > 1)
        {
            // �����ߴٸ� � �������� ���� �Ұ��� üũ 
            whatItem = Random.Range(0, 3);
            
            // 0�� ������, ����
            if(whatItem == 0)
            {
                items[whatItem].SetActive(true);
                items[1].SetActive(false);
                items[2].SetActive(false);
            }
            // 1�� ������, ��1
            else if (whatItem == 1)
            {
                items[whatItem].SetActive(true);
                items[0].SetActive(false);
                items[2].SetActive(false);
            }

            // 2�� ������, ��2
            else
            {
                items[whatItem].SetActive(true);
                items[0].SetActive(false);
                items[1].SetActive(false);
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
