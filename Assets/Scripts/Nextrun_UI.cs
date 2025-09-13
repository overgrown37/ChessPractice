using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Next_turn_UI : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float pauseBefore = 0.8f;   // 턴 끝난 뒤 잠깐 멈춤
    [SerializeField] private float slideDuration = 0.35f;
    [SerializeField] private float holdDuration = 0.8f;

    [Header("Easing")]
    [SerializeField] private Ease easeIn = Ease.OutCubic;
    [SerializeField] private Ease easeOut = Ease.InCubic;

    [Header("Extra")]
    [SerializeField] private float extraLeftMargin = 120f; // 화면 밖 숨김 여유

    RectTransform panel;
    CanvasGroup cg;
    Vector2 onPos, offPos;
    bool busy;

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        // 화면 안 “정착 위치”는 에디터에서 배치한 그대로
        onPos = panel.anchoredPosition;

        // 왼쪽으로 패널너비 + 여유만큼 밀어 숨김 위치
        float dist = panel.rect.width + extraLeftMargin;
        offPos = onPos + Vector2.left * dist;

        // 시작은 숨겨둠
        panel.anchoredPosition = offPos;
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    /// <summary>
    /// 배너를 한번 보여주고 다시 숨김. 끝날 때까지 대기하려면
    /// GameManager 쪽에서 `yield return StartCoroutine(ShowAndHide());`
    /// </summary>
    public IEnumerator ShowAndHide()
    {
        if (busy) yield break;
        busy = true;

        // 1) 잠깐 멈춤
        yield return new WaitForSecondsRealtime(pauseBefore);

        // 2) 슬라이드 인 + 페이드 인
        cg.blocksRaycasts = true; cg.interactable = true;
        cg.DOFade(1f, 0.15f).SetUpdate(true);
        yield return panel.DOAnchorPos(onPos, slideDuration)
                          .SetEase(easeIn)
                          .SetUpdate(true)
                          .WaitForCompletion();

        // 3) 유지
        yield return new WaitForSecondsRealtime(holdDuration);

        // 4) 슬라이드 아웃 + 페이드 아웃
        cg.DOFade(0f, 0.15f).SetUpdate(true);
        yield return panel.DOAnchorPos(offPos, slideDuration)
                          .SetEase(easeOut)
                          .SetUpdate(true)
                          .WaitForCompletion();
        cg.blocksRaycasts = false; cg.interactable = false;

        busy = false;
    }
}
