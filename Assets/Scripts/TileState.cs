using UnityEngine;

public class TileState : MonoBehaviour// 타일 상태 관리 스크립트
{
    private TileCoord tileCoord;// 타일 좌표를 관리하는 스크립트

    void Start()
    {
        tileCoord = gameObject.GetComponent<TileCoord>();
    }

    private void OnMouseUp()// 마우스 클릭 시 호출되는 함수
    {
        // UI 위에서 클릭된 경우 입력 무시
        if (UnityEngine.EventSystems.EventSystem.current != null && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (tileCoord.IsMove())// 타일이 이동 가능한 상태인지 확인
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {
                Chesspiece cp = selectedPiece.GetComponent<Chesspiece>();// 선택된 체스말의 컴포넌트를 가져옴
                int prevX = cp.GetXBoard();// 선택된 체스말의 이전 X 좌표
                int prevY = cp.GetYBoard();// 선택된 체스말의 이전 Y 좌표

                GameManager.instance.SetPositionEmpty(prevX, prevY); // 1. 원래 위치 비우기

                cp.SetXBoard(tileCoord.GetXBoard()); // 2. 새 좌표로 변경
                cp.SetYBoard(tileCoord.GetYBoard());

                GameManager.instance.SetPosition(selectedPiece);     // 3. 새 위치에 배치
                cp.SetCoords();                                     // 4. 화면상의 위치 갱신

                GameManager.instance.GetComponent<SetMovePlate>().ClearMovePlates();
                GameManager.instance.GetComponent<SelectManager>().SetEmptySelectedPiece(); // 5. 선택된 말 비우기
                GameManager.instance.GetComponent<PlayerManager>().NextPlayer(); // 6. 다음 플레이어로 전환
            }
        }
        else if (tileCoord.IsAttack())// 타일이 공격 가능한 상태인지 확인
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {

            }
        }
        else if (tileCoord.IsRangedAttack())// 타일이 범위 공격 가능한 상태인지 확인
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {

            }
        }
    }
}
