using Unity.VisualScripting;
using UnityEngine;

public class SetRangedAttackPlate : MonoBehaviour
{
    [SerializeField]
    private int xBoard = -1;
    [SerializeField]
    private int yBoard = -1;

    public GameObject RangedAttackPlatePrefab = null;
    public bool fanAttack = false;
    public bool roundAttack = false;
    public int roundRange = 1;

    public float delayTime = 0.3f;
    private float timer = 0.0f;
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

    public void FanRangedAttackPlate()
    {
        fanAttack = true;
    }

    public void RoundAttackPlate(int range)
    {
        roundAttack = true;
        roundRange = range;
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

    public string GetMouseDirectionFromPiece()
    {
        // 1. 마우스의 월드 좌표를 가져옴
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2. 마우스 위치에 가장 가까운 타일 좌표 계산 (타일의 크기와 위치에 따라 보정 필요)
        // 예시: 타일이 (0,0)~(7,7) 정수 좌표에 정렬되어 있다고 가정
        int mouseX = Mathf.RoundToInt(mouseWorldPos.x + 3.5f); // 보드 위치에 맞게 조정
        int mouseY = Mathf.RoundToInt(mouseWorldPos.y + 3.5f);

        // 3. 방향 계산
        int dx = mouseX - xBoard;
        int dy = mouseY - yBoard;

        if (Mathf.Abs(dx) > Mathf.Abs(dy))
        {
            if (dx > 0)
                return "right";
            else if (dx < 0)
                return "left";
        }
        else if (Mathf.Abs(dy) > 0)
        {
            if (dy > 0)
                return "up";
            else if (dy < 0)
                return "down";
        }
        return "center"; // 같은 타일 위에 있을 때
    }

    public void showUI()
    {
        TileCoord[] EveryTiles = FindObjectsByType<TileCoord>(FindObjectsSortMode.None);
        foreach (var tile in EveryTiles)
        {
            if (tile.IsRangedAttack())
            {
                if (tile.GetComponent<TileCoord>().GetChesspiece() != null)
                {
                    tile.GetComponent<TileCoord>().GetChesspiece().GetComponent<Chesspiece>().ShowUIDamage(GameManager.instance.GetComponent<SelectManager>().GetSkillDamageSelectedPiece());
                }
            }
        }
    }

    public void hideUI()
    {
        GameObject[] EveryPiece = GameObject.FindGameObjectsWithTag("Chesspiece");
        foreach (var c in EveryPiece)
        {
            c.GetComponent<Chesspiece>().HideUIHover();
        }
    }
    private void Start()
    {
        timer = 0.3f;
    }
    private void Update()
    {
        //hideUI();
        if (fanAttack)
        {
            ClearRangedAttackPlates(); // 기존의 공격 타일 제거
            
            GetPosition(); // 현재 선택된 체스말의 좌표를 가져옴
            string direction = GetMouseDirectionFromPiece(); // 마우스 방향을 가져옴
            // 방향에 따라 공격 타일 생성
            switch (direction)
            {
                case "up":
                    PointRangedAttackPlate(0, 1);
                    PointRangedAttackPlate(1, 2);
                    PointRangedAttackPlate(-1, 2);
                    PointRangedAttackPlate(0, 2);
                    break;
                case "down":
                    PointRangedAttackPlate(0, -1);
                    PointRangedAttackPlate(1, -2);
                    PointRangedAttackPlate(-1, -2);
                    PointRangedAttackPlate(0, -2);
                    break;
                case "left":
                    PointRangedAttackPlate(-1, 0);
                    PointRangedAttackPlate(-2, 1);
                    PointRangedAttackPlate(-2, -1);
                    PointRangedAttackPlate(-2, 0);
                    break;
                case "right":
                    PointRangedAttackPlate(1, 0);
                    PointRangedAttackPlate(2, 1);
                    PointRangedAttackPlate(2, -1);
                    PointRangedAttackPlate(2, 0);
                    break;
                case "center":
                    PointRangedAttackPlate(0, 1);
                    PointRangedAttackPlate(1, 2);
                    PointRangedAttackPlate(-1, 2);
                    PointRangedAttackPlate(0, 2);
                    break;
            }
        }
        else if (roundAttack)
        {
            ClearRangedAttackPlates(); // 기존의 공격 타일 제거

            // 1. 마우스의 월드 좌표를 가져옴
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 2. 마우스 위치에 가장 가까운 타일 좌표 계산 (보드 위치에 맞게 조정)
            int mouseX = Mathf.RoundToInt(mouseWorldPos.x + 3.5f);
            int mouseY = Mathf.RoundToInt(mouseWorldPos.y + 3.5f);

            // 3. 마우스 아래 타일과 인접 4방향 타일에 범위 공격 타일 생성
            int[,] deltas = { {0,0}, {0,1}, {0,-1}, {1,0}, {-1,0} };
            for (int i = 0; i < deltas.GetLength(0); i++)
            {
                int tx = mouseX + deltas[i,0];
                int ty = mouseY + deltas[i,1];
                if (GameManager.instance.PositionOnBoard(tx, ty))
                {
                    CreateRangedAttackPlate(tx, ty);
                }
            }
        }
        timer += Time.deltaTime; // 누적 시간 증가
        if (timer > delayTime)
        {
            showUI();
            //Debug.Log("껄껄");
            timer = 0.0f;
        }
    }
}
