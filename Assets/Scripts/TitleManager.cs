using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 필요

public class TitleManager : MonoBehaviour
{
    // Start 버튼 클릭 시 호출될 함수
    public void StartGame()
    {
        // "GameScene"이라는 이름의 씬으로 로드합니다.
        // 씬 이름이 정확해야 합니다!
        SceneManager.LoadScene("Game");
    }

    // End 버튼 클릭 시 호출될 함수
    public void ExitGame()
    {
        // 에디터에서 실행 중일 때는 에디터 모드를 종료
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        // 빌드된 게임에서는 애플리케이션 종료
        #else
            Application.Quit();
        #endif
    }
}