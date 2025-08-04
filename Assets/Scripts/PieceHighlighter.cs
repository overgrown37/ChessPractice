using DG.Tweening;
using UnityEngine;

public class PieceHighlighter : MonoBehaviour
{
    private Tween bounceTween;
    private Chessman chessman = null;

    private void Start()
    {
        chessman = GetComponent<Chessman>();
    }

    public void Select()
    {
        // 이미 트윈이 있으면 중복 방지
        if (bounceTween != null && bounceTween.IsPlaying()) return;

        bounceTween = transform.DOMoveY(transform.position.y + 0.05f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void Deselect()
    {
        if (bounceTween != null) bounceTween.Kill();
        transform.position = chessman.Coords;
    }
}