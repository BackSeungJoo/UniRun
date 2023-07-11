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
        // 플레이어가 젬1을 먹었을 때,
        if (collision.tag == "Player")
        {
            GFunc.Log("젬 1에 닿았다! 점수 3점");
            gameObject.SetActive(false);

            // 점수 3 증가
            GameManager.instance.AddScore(3);
        }
    }
}
