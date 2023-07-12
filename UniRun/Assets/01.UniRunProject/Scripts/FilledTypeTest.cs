using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FilledTypeTest : MonoBehaviour
{
    public Image filledTypeImg;

    private void Awake()
    {
        filledTypeImg.fillAmount = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PassedCoolTime(0.25f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //if(0 < filledTypeImg.fillAmount)
        //{
        //    filledTypeImg.fillAmount -= (Time.deltaTime * 0.5f);
        //}
    }

    // 코루틴
    private IEnumerator PassedCoolTime(float cooltimeDelay)
    {
        float cooltimePercent = 1f / 10f;
        
        while(0 < filledTypeImg.fillAmount)
        {
            // 이만큼 시간이 걸린다.
            yield return new WaitForSeconds(cooltimeDelay);

            // 시간이 지나간 다음에 처리한다.
            filledTypeImg.fillAmount -= cooltimePercent;
        }
    }
}
