using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
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
        // �÷��̾ ��2�� �Ծ��� ��,
        if (collision.tag == "Player")
        {
            GFunc.Log("Ŭ�ι��� �Ծ���.");
            gameObject.SetActive(false);
        }
    }
}
