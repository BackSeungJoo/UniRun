using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float width;    // ����� ���� ����

    private void Awake()
    {
        // BoxColiider2D ������Ʈ�� size �ʵ��� x ���� ���� ���̷� ���
        BoxCollider2D backGroundCollider = GetComponent<BoxCollider2D>();
        width = backGroundCollider.size.x;
    }


    // Update is called once per frame
    private void Update()
    {
        // ���� ��ġ�� �������� �������� width �̻� �̵����� �� ��ġ�� ���ġ
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        // ���� ��ġ���� ���������� ���� ���� *2 ��ŭ �̵�
        Vector2 offset = new Vector2(width * 2f, 0);

        // transform.position = (Vector2)transform.position + offset; ������
        transform.position = transform.position.AddVector(offset);
    }
}
