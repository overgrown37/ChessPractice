using UnityEngine;
using UnityEngine.EventSystems;

public class PieceSelecter : MonoBehaviour// 체스말 선택 스크립트
{
    private void OnMouseUp()// 마우스 클릭 시 호출되는 함수
    {
        // UI 위에서 클릭된 경우 입력 무시
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        // 현재 기물이 위치한 타일 가져오기
        Chesspiece piece = gameObject.GetComponent<Chesspiece>();
        int x = piece.GetXBoard();
        int y = piece.GetYBoard();
        GameObject tile = GameManager.instance.positions[x, y];

        // 만약 이 타일이 공격 타일(AttackPlate 또는 RangedAttackPlate) 상태라면, 타일 클릭과 동일하게 처리
        TileCoord tileCoord = tile.GetComponent<TileCoord>();
        if (tileCoord != null && (tileCoord.IsAttack() || tileCoord.IsRangedAttack()))
        {
            TileState tileState = tile.GetComponent<TileState>();
            tileState.OnMouseUp(); // 타일의 클릭 이벤트 직접 호출
            return;
        }

        // 일반 선택 로직
        if (!GameManager.instance.GetComponent<GameOverController>().IsGameOver() //게임 오버 되었는지
            &&
            GameManager.instance.GetComponent<PlayerManager>().GetPlayer() == piece.player)//현재 플레이어가 선택한 체스말과 같은 편인지
        {
            GameManager.instance.GetComponent<SelectManager>().SetSelectedPiece(gameObject);// 선택된 체스말 설정
        }
    }
}
