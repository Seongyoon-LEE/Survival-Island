using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Text killTXT; // 킬수를 표시할 UI 텍스트

    
    void Start()
    {
        killTXT.text = $"Kill: <color=#f00>{GameManager.Instance.totalkill.ToString()}</color>";
        GameManager.Instance.MouseCursorVisible();
    }

    

    public void PlaySceneMove()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 게임 종료
#else
        Application.Quit();
#endif
    }
}
