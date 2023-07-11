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
        // 플레이어가 깃털을 먹었을 때, 
        if (collision.tag == "Player")
        {
            GFunc.Log("깃털에 닿았다. 점프카운트 초기화");
            gameObject.SetActive(false);

            // 점프 카운트 감소
            playerController.jumpCount = 0;
        }
    }
}
