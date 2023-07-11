using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem2 : MonoBehaviour
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
        // 플레이어가 젬2을 먹었을 때,
        if (collision.tag == "Player")
        {
            GFunc.Log("젬 2에 닿았다! 점수 5점");
            gameObject.SetActive(false);

            // 점수 5 증가
            GameManager.instance.AddScore(5);
        }
    }
}
