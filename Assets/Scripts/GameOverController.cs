using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject panel;            // End_game_UI
    [SerializeField] private CanvasGroup group;           // End_game_UI에 붙은 CanvasGroup
    [SerializeField] private TextMeshProUGUI winnerText;  // TMP 텍스트
    [SerializeField] private TextMeshProUGUI reasonText;  // TMP 텍스트
    [SerializeField] private float fadeDuration = 0.35f;
    private bool isGameOver = false;

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver(bool value)
    {
        isGameOver = value;
    }
    public void Show(string winner, string reason)
    {
        if (panel && !panel.activeSelf) panel.SetActive(true);

        if (winnerText) winnerText.text = $"{winner} Wins!";
        if (reasonText) reasonText.text = reason;

        if (group)
        {
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = true;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            group.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        group.interactable = true;
    }

    public void OnClickRestart()
    {
        // 기존 싱글톤 오브젝트 파괴
        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.DestroySelf();

        var cur = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cur.buildIndex);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
