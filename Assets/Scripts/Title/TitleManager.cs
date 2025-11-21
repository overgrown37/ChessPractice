using UnityEngine;
using UnityEngine.SceneManagement; 

public class TitleManager : MonoBehaviour
{
    // Start 버튼 클릭 시 호출될 함수
    public void StartGame()
    {
        // SceneTransitionManager의 LoadScene 함수를 호출하여 페이드 효과와 함께 씬 전환
        if (SceneTransitionManager.instance != null)
        {
            SceneTransitionManager.instance.LoadScene("Game");
        }
        else
        {
            // SceneTransitionManager가 없으면 그냥 바로 씬 전환 (fallback)
            SceneManager.LoadScene("Game");
        }
    }

    // End 버튼 클릭 시 호출될 함수
    public void ExitGame()
    {
      
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}