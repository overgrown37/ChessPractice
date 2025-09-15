using UnityEngine;

public class SetRangedAttackPlate : MonoBehaviour
{
    [SerializeField]
    private int xBoard = -1;
    [SerializeField]
    private int yBoard = -1;

    public GameObject RangedAttackPlatePrefab = null;

    public void GetPosition()// 현재 선택된 체스말의 좌표를 가져옴
    {
        GameObject sp = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
        xBoard = sp.GetComponent<Chesspiece>().GetXBoard();
        yBoard = sp.GetComponent<Chesspiece>().GetYBoard();
    }

    public void PointRangedAttackPlate(int x, int y)// 해당 좌표에 공격 가능한 타일을 표시
    {
        if (GameManager.instance.PositionOnBoard(xBoard + x, yBoard + y))//MovePlate와 다르게 공격은 적이 있든 없든 표시
        {
            CreateRangedAttackPlate(xBoard + x, yBoard + y);
        }
    }

    public void LineRangedAttackPlate(int xIncrement, int yIncrement)// 직선 방향으로 공격 가능한 타일을 표시. 계속 나아간다.
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (GameManager.instance.PositionOnBoard(x, y))
        {
            CreateRangedAttackPlate(x, y);
            x += xIncrement;
            y += yIncrement;
        }
    }

    public void CreateRangedAttackPlate(int x, int y)// 타일의 위치에 이동 가능한 타일을 생성
    {
        // 타일의 월드 좌표를 가져옴
        Vector3 tilePos = GameManager.instance.positions[x, y].transform.position;
        // attackPlate를 타일의 위치 위에(조금 위로 띄우고 싶으면 z값만 조정) 생성(알 수 없는 이유로 살짝 위치가 이상해져서 보정치를 넣음, 아는 거 있으면 알려주세요)
        Vector3 spawnPos = new Vector3(tilePos.x - 0.0055f, tilePos.y - 0.0055f, tilePos.z - 3.0f);

        GameManager.instance.positions[x, y].GetComponent<TileCoord>().SetRangedAttack();

        GameObject mp = Instantiate(
            RangedAttackPlatePrefab,
            spawnPos,
            Quaternion.identity
        );
    }

    public void ClearRangedAttackPlates()// 공격 가능한 타일을 모두 제거
    {
        GameObject[] rangedAttackPlates = GameObject.FindGameObjectsWithTag("RangedAttackPlate");
        foreach (GameObject rangedAttackPlate in rangedAttackPlates)
        {
            Destroy(rangedAttackPlate);
        }
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject Tile in Tiles)
        {
            Tile.GetComponent<TileCoord>().InitState();
        }
    }
}
