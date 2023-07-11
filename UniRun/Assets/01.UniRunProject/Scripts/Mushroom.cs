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
        // 플레이어가 젬2을 먹었을 때,
        if (collision.tag == "Player")
        {
            GFunc.Log("클로버를 먹었다.");
            gameObject.SetActive(false);
        }
    }
}
