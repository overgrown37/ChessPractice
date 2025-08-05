using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    private GameObject currentTile = null;

    void Update()
    {
        GameObject tileUnderCursor = null;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Tile"))
            {
                tileUnderCursor = hit.collider.gameObject;
                break;
            }
        }

        if (tileUnderCursor != currentTile)
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
