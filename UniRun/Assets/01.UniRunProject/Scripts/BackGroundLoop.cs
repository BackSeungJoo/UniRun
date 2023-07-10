using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float width;    // 배경의 가로 길이

    private void Awake()
    {
        // BoxColiider2D 컴포넌트의 size 필드의 x 값을 가로 길이로 사용
        BoxCollider2D backGroundCollider = GetComponent<BoxCollider2D>();
        width = backGroundCollider.size.x;
    }


    // Update is called once per frame
    private void Update()
    {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을 때 위치를 재배치
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        // 현재 위치에서 오른쪽으로 가로 길이 *2 만큼 이동
        Vector2 offset = new Vector2(width * 2f, 0);

        // transform.position = (Vector2)transform.position + offset; 래핑함
        transform.position = transform.position.AddVector(offset);
    }
}
