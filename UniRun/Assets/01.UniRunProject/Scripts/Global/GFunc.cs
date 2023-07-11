using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// monobehavior�� ��� ���� �ʿ䰡 ����.
public static partial class GFunc
{
    // ! �޼����� ǥ���մϴ�.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    // ! �����ڿ��� ��� ǥ���ϱ� ���� ���˴ϴ�.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif
    }

    // ! ������ ����(False)�� ��� ���α׷� ������ �ߴܽ�Ű�� ���� �޽����� ǥ���մϴ�.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE
        Debug.Assert(condition);
#endif
    }

    //! GameObject �޾Ƽ� Text ������Ʈ ã�Ƽ� text �ʵ� �� �����ϴ� �Լ�
    public static void SetText(this GameObject target, string text)
    {
        Text textComponent = target.GetComponent<Text>();
        if (textComponent == null || textComponent == default) { return; }

        textComponent.text = text;
    }

    //! LoadScene �Լ� �����Ѵ�.
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //! ���� ���� �̸��� �����Ѵ�.
    public static string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //! �� ���͸� ���Ѵ�. 
    // Ȯ�� �޼�����.
    public static Vector2 AddVector(this Vector3 origin, Vector2 addVector)
    {
        Vector2 result = new Vector2(origin.x, origin.y);
        result += addVector;
        return result;
    }
}