using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// monobehavior를 상속 받을 필요가 없다.
public static partial class GFunc
{
    // ! 메세지를 표시합니다.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    // ! 개발자에게 경고를 표시하기 위해 사용됩니다.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif
    }

    // ! 조건이 거짓(False)인 경우 프로그램 실행을 중단시키고 에러 메시지를 표시합니다.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE
        Debug.Assert(condition);
#endif
    }

    //! GameObject 받아서 Text 컴포넌트 찾아서 text 필드 값 수정하는 함수
    public static void SetText(this GameObject target, string text)
    {
        Text textComponent = target.GetComponent<Text>();
        if (textComponent == null || textComponent == default) { return; }

        textComponent.text = text;
    }

    //! LoadScene 함수 래핑한다.
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //! 현재 씬의 이름을 리턴한다.
    public static string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //! 두 벡터를 더한다. 
    // 확장 메서드임.
    public static Vector2 AddVector(this Vector3 origin, Vector2 addVector)
    {
        Vector2 result = new Vector2(origin.x, origin.y);
        result += addVector;
        return result;
    }

    // ! 컴포넌트가 유효한지 여부를 체크하는 함수
    public static bool IsValid<T>(this T target) where T : Component
    {
        if (target == null || target == default) { return false; }
        else { return true; }
    }

    // ! 리스트가 유효한지 여부를 체크하는 함수
    public static bool IsValid<T>(this List<T> target)
    {
        // 리스트가 인스턴스화 되어있는지 체크하고
        bool isInvalid = (target == null || target == default);
        // 리스트의 엘리먼트가 제로인지 체크하고,
        isInvalid = isInvalid || target.Count == 0;

        // 쓸 수 있다고 판단하는 것.
        if (target == null || target == default) { return false; }
        else { return true; }
    }
}