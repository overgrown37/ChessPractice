using UnityEngine;
using UnityEngine.EventSystems;

public class PieceSelecter : MonoBehaviour// 체스말 선택 스크립트
{
    private void OnMouseUp()// 마우스 클릭 시 호출되는 함수
    {
        // UI 위에서 클릭된 경우 입력 무시
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (!GameManager.instance.GetComponent<GameOverController>().IsGameOver() //게임 오버 되었는지
            &&
            GameManager.instance.GetComponent<PlayerManager>().GetPlayer() == gameObject.GetComponent<Chesspiece>().player)//현재 플레이어가 선택한 체스말과 같은 편인지
        {
            GameManager.instance.GetComponent<SelectManager>().SetSelectedPiece(gameObject);// 선택된 체스말 설정
        }
    }
}
