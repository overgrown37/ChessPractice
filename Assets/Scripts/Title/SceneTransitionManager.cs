using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    public Image fadePanel;

    public float fadeDuration = 0.5f;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        FadeIn();
    }

    public void LoadScene(string sceneName)
    {
        //페이드 아웃이 완료 되었다면 씬 전환
        FadeOut(() =>
        {
           SceneManager.LoadScene(sceneName);
        });
    }

    private void FadeIn()
    {
        if (fadePanel == null) return;

        // 불투명 -> 투명 (알파 값 1 -> 0)
        fadePanel.DOFade(0f, fadeDuration).From(1f).SetEase(Ease.OutQuad);
    }

    private void FadeOut(System.Action onComplete = null)
    {
        if (fadePanel == null) return;
      
        // 투명 -> 불투명 (알파 값 0 -> 1)
        fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새로운 씬이 로드될 때마다 페이드 인을 시작합니다.
        // 이때 해당 씬의 FadePanel을 찾아 연결해야 합니다.
        Canvas currentCanvas = FindObjectOfType<Canvas>();
        if (currentCanvas != null)
        {
            fadePanel = currentCanvas.transform.Find("FadePanel")?.GetComponent<Image>();
            if (fadePanel != null)
            {
                // 새 씬 로드 시 FadePanel의 알파 값을 1로 초기화하여 다시 페이드 인
                Color panelColor = fadePanel.color;
                panelColor.a = 1f;
                fadePanel.color = panelColor;
                FadeIn();
            }
        }
    }
}
