using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글턴을 할당할 전역 변수
    public static GameManager instance;

    // 게임 오버 상태
    public bool isGameOver = false;

    // 점수를 출력할 UI 텍스트
    public TMP_Text scoreText;  // Text mesh pro 컴포넌트 쓴 경우
    //public Text scoreText_;   // Legacy Text 컴포넌트 쓴 경우

    // 게임오버시 활성화할 UI 게임 오브젝트
    public GameObject gameoverUI;

    // 게임 점수
    public int score = 0; 

    // 게임 시작과 동시에 싱글턴을 구성
    private void Awake()
    {
        // 싱글턴 변수 instance가 비어 있는가?
        if (instance.IsValid() == false)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }

        // instance에 이미 다른 GameManager 오브젝트가 할당 되어 있는 경우
        else
        {
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미
            // 싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            GFunc.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다.!");
            Destroy(gameObject);
        }

        //List<int> intList = null;
        //List<int> intList = new List<int>();
        //intList.Add(0);

        //Debug.LogFormat("intList가 유효한지? (존재하는지?) : {0}", intList.IsValid());
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver && Input.GetMouseButtonDown(0))
        {
            // 게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
            GFunc.LoadScene(GFunc.GetActiveSceneName());

            // ! 코드 래핑 전원형
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore)
    {
        // 게임오버가 아니라면
        if(isGameOver == false)
        {
            // 점수를 증가
            score += newScore;
            scoreText.text = string.Format("Score : {0}", score);
        }
    }

    // 해당 함수가 실행되면 isGameOver가 true가 되고, gameoverUI에 할당된 Gameover Text 게임 오브젝트가 활성화 됩니다.
    public void OnPlayerDead()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
    }
}
