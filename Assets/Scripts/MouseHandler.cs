using JetBrains.Annotations;
using UnityEngine;

public class MouseHandler : MonoBehaviour// 마우스 커서가 타일 위에 있을 때 타일 색상을 변경하는 스크립트 CsorManage.cs와 혼동 주의 합쳐도 좋을 듯?
{
    private GameObject currentTile = null;// 현재 마우스 커서 아래에 있는 타일
    public GameObject currentPiece = null;//
    public GameObject prevPiece = null;

    void Update()
    {
        GameObject tileUnderCursor = null;// 현재 마우스 커서 아래에 있는 타일을 저장할 변수
        GameObject PieceOnTile = null;//
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// 마우스 위치를 월드 좌표로 변환

        if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            // 선택 해제
            GameManager.instance.GetComponent<SelectManager>().SetEmptySelectedPiece();
            // 콜라이더 다시 활성화
            GameManager.instance.SetAllChesspieceCollidersEnabled(true);
            // (타일 상태 초기화는 SetEmptySelectedPiece에서 이미 처리됨)
            GameManager.instance.GetComponent<ButtonManager>().DeactiveButton();
            GameManager.instance.GetComponent<SetRangedAttackPlate>().fanAttack = false;
            GameManager.instance.GetComponent<SetRangedAttackPlate>().roundAttack = false;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);// 마우스 위치에서 Raycast를 사용하여 충돌하는 모든 오브젝트를 감지

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Tile"))// 충돌한 오브젝트 중 "Tile" 태그를 가진 오브젝트만 처리
            {
                tileUnderCursor = hit.collider.gameObject;// 타일 오브젝트를 저장
                PieceOnTile = tileUnderCursor.GetComponent<TileCoord>().GetChesspiece();//
                break;
            }
        }

        if (tileUnderCursor != currentTile)// 현재 타일과 마우스 커서 아래의 타일이 다를 때만 색상 변경
        {
            // 이전 타일 원래 색으로 복구
            if (currentTile != null)
            {
                TileColor prevTile = currentTile.GetComponent<TileColor>();
                if (prevTile != null)
                    prevTile.SetOriginalColor();
            }

            // 새 타일 하이라이트
            if (tileUnderCursor != null)
            {
                TileColor newTile = tileUnderCursor.GetComponent<TileColor>();
                if (newTile != null)
                    newTile.SetHighlight();
                if (tileUnderCursor.GetComponent<TileCoord>().GetChesspiece())
                {
                    GameObject[] EveryPiece = GameObject.FindGameObjectsWithTag("Chesspiece");
                    foreach (var c in EveryPiece)
                    {
                        c.GetComponent<Chesspiece>().HideUIHover();
                    }
                }
            }

            // 현재 타일 업데이트
            currentTile = tileUnderCursor;
        }
        
        if (currentPiece != PieceOnTile)
        {
            // 이전 기물 UI 끄기
            if (prevPiece != null)
            {
                GameObject[] EveryPiece = GameObject.FindGameObjectsWithTag("Chesspiece");
                foreach (var c in EveryPiece)
                {
                    c.GetComponent<Chesspiece>().HideUIHover();
                }
            }

            // 새 기물 UI 켜기
            if (PieceOnTile != null)
            {
                if (currentTile.GetComponent<TileCoord>().IsAttack())
                {
                    //공격중일때
                    int damage = GameManager.instance.GetComponent<SelectManager>().GetSkillDamageSelectedPiece();
                    PieceOnTile.GetComponent<Chesspiece>().ShowUIDamage(damage);
                }
                else
                {
                    PieceOnTile.GetComponent<Chesspiece>().ShowUIHover();
                }                 
            }

            prevPiece = PieceOnTile;
            currentPiece = PieceOnTile;
        }
    }
}
