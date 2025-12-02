using System.Collections.Generic;
using UnityEngine;

public class MoveRangeCalculate : MonoBehaviour
{
    public List<Vector2Int> GetMovableTilesWithinManhattanDistance(int startX, int startY, int moveDistance)
    {
        List<Vector2Int> possibleTiles = new List<Vector2Int>();

        // 맨해튼 거리가 moveDistance 이하인 모든 타일을 검사
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // 맨해튼 거리 계산: |x1 - x2| + |y1 - y2|
                int manhattanDistance = Mathf.Abs(startX - x) + Mathf.Abs(startY - y);

                // 맨해튼 거리가 이동력 이하이고, 보드 범위 내에 있는 경우
                if (manhattanDistance <= moveDistance && manhattanDistance > 0)
                {
                    possibleTiles.Add(new Vector2Int(x, y));
                }
            }
        }

        return possibleTiles;
    }
}
