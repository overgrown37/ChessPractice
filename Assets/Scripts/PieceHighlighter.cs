using DG.Tweening;
using UnityEngine;

public class PieceHighlighter : MonoBehaviour// 체스말 하이라이트 스크립트
{
    private Tween bounceTween;
    private Chesspiece chess = null;

    private void Start()
    {
        chess = GetComponent<Chesspiece>();
    }

    public void Select()// 선택
    {
        if (bounceTween != null && bounceTween.IsPlaying()) return;// 중복 방지

        bounceTween = transform.DOMoveY(transform.position.y + 0.05f, 0.5f)// Y축으로 0.05f 만큼 이동
            .SetLoops(-1, LoopType.Yoyo)// 무한 반복
            .SetEase(Ease.Linear);
    }

    public void Deselect()// 선택 해제
    {
        if (bounceTween != null) bounceTween.Kill();// Tween 중지
        transform.position = chess.Coords;// 원래 위치로 되돌리기
    }
}