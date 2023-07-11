using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    public GameObject[] items;          // 아이템 오브젝트
    public GameObject[] obstacles;      // 장애물 오브젝트
    private bool stepped = false;       // 플레이어 캐릭터가 밟았는가?

    private int itemYes = default;  // 아이템 생성 여부 판단
    private int whatItem0 = default; // 어떤 아이템 생성할 건지
    private int whatItem1 = default; // 어떤 아이템 생성할 건지
    private int whatItem2 = default; // 어떤 아이템 생성할 건지
    private int whatItem3 = default; // 어떤 아이템 생성할 건지

    // 컴포넌트가 활성화될 때마다 매번 실행되는 메서드
    private void OnEnable()
    {
        // 밟힘 상태를 리셋
        stepped = false;

        // 장애물의 수만큼 루프
        for (int i = 0; i < obstacles.Length; i++)
        {
            // 현재 순번의 장애물을 1/5의 확률로 활성화
            if(Random.Range(0, 5) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }

        // 랜덤 아이템 생성
        // 아이템을 생성할건지 아닌지 여부 체크
        itemYes = Random.Range(0, 7);

        // 1이상이면 아이템을 생성합니다.
        if (itemYes > 1)
        {
            // 생성했다면 어떤 아이템을 생성 할건지 체크 
            whatItem0 = Random.Range(0, 3);
            whatItem1 = Random.Range(0, 2);
            whatItem2 = Random.Range(0, 2);
            whatItem3 = Random.Range(0, 8);
            
            // 0번 아이템, 깃털
            if(whatItem0 == 0)
            {
                items[0].SetActive(true);
            }
            else
            {
                items[0].SetActive(false);
            }
            // 1번 아이템, 젬1
            if (whatItem1 == 0)
            {
                items[1].SetActive(true);
            }
            else
            {
                items[1].SetActive(false);
            }

            // 2번 아이템, 젬2
            if (whatItem2 == 0)
            {
                items[2].SetActive(true);
            }
            else
            {
                items[2].SetActive(false);
            }

            // 3번 아이템 클로버
            if (whatItem3 == 0)
            {
                items[3].SetActive(true);
            }
            else
            {
                items[3].SetActive(false);
            }
        }

        // 아니라면 리턴
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
        // 충돌한 상대방의 태그가 Player이고 이전에 플레이어 캐릭터가 밟지 않았다면
        if((collision.collider.tag == "Player") && (stepped == false))
        {
            // 점수를 추가하고 밟힘 상태를 참으로 변경
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
