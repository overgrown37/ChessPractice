using UnityEngine;

public class MouseHandler : MonoBehaviour// 마우스 커서가 타일 위에 있을 때 타일 색상을 변경하는 스크립트 CsorManage.cs와 혼동 주의 합쳐도 좋을 듯?
{
    private GameObject currentTile = null;// 현재 마우스 커서 아래에 있는 타일
    void Update()
    {
        GameObject tileUnderCursor = null;// 현재 마우스 커서 아래에 있는 타일을 저장할 변수

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// 마우스 위치를 월드 좌표로 변환

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);// 마우스 위치에서 Raycast를 사용하여 충돌하는 모든 오브젝트를 감지

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Tile"))// 충돌한 오브젝트 중 "Tile" 태그를 가진 오브젝트만 처리
            {
                tileUnderCursor = hit.collider.gameObject;// 타일 오브젝트를 저장
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
            }

            // 현재 타일 업데이트
            currentTile = tileUnderCursor;

        }
    }
}
