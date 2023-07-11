using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱����� �Ҵ��� ���� ����
    public static GameManager instance;

    // ���� ���� ����
    public bool isGameOver = false;

    // ������ ����� UI �ؽ�Ʈ
    public TMP_Text scoreText;  // Text mesh pro ������Ʈ �� ���
    //public Text scoreText_;   // Legacy Text ������Ʈ �� ���

    // ���ӿ����� Ȱ��ȭ�� UI ���� ������Ʈ
    public GameObject gameoverUI;

    // ���� ����
    public int score = 0; 

    // ���� ���۰� ���ÿ� �̱����� ����
    private void Awake()
    {
        // �̱��� ���� instance�� ��� �ִ°�?
        if (instance.IsValid() == false)
        {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
        }

        // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ� �Ǿ� �ִ� ���
        else
        {
            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            GFunc.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�.!");
            Destroy(gameObject);
        }

        //List<int> intList = null;
        //List<int> intList = new List<int>();
        //intList.Add(0);

        //Debug.LogFormat("intList�� ��ȿ����? (�����ϴ���?) : {0}", intList.IsValid());
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver && Input.GetMouseButtonDown(0))
        {
            // ���ӿ��� ���¿��� ���콺 ���� ��ư�� Ŭ���ϸ� ���� �� �����
            GFunc.LoadScene(GFunc.GetActiveSceneName());

            // ! �ڵ� ���� ������
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore)
    {
        // ���ӿ����� �ƴ϶��
        if(isGameOver == false)
        {
            // ������ ����
            score += newScore;
            scoreText.text = string.Format("Score : {0}", score);
        }
    }

    // �ش� �Լ��� ����Ǹ� isGameOver�� true�� �ǰ�, gameoverUI�� �Ҵ�� Gameover Text ���� ������Ʈ�� Ȱ��ȭ �˴ϴ�.
    public void OnPlayerDead()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
    }
}
