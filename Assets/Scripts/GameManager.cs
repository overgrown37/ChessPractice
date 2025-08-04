using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱��� ����
    public static GameManager instance { get; private set; } 
    public GameObject chesspiece;
    public GameObject tile;
    public GameObject attackButton;
    public GameObject moveButton;
    public bool selected = false;
    public Chessman selectedPeice = null;

    public Tile[,] positions = new Tile[8, 8];
    private GameObject[] playerBlack;
    private GameObject[] playerWhite;

    private string currentPlayer = "white";

    private bool gameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� GameManager �ı�
        }
    }

    void Start()
    {
        attackButton.SetActive(false);
        moveButton.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                positions[i,j] = CreateBoard(i, j);
            }
        }

        playerWhite = new GameObject[] { AllocateChesspiece("white_rook", 0, 0), AllocateChesspiece("white_knight", 1, 0),
            AllocateChesspiece("white_bishop", 2, 0), AllocateChesspiece("white_queen", 3, 0), AllocateChesspiece("white_king", 4, 0),
            AllocateChesspiece("white_bishop", 5, 0), AllocateChesspiece("white_knight", 6, 0), AllocateChesspiece("white_rook", 7, 0),
            AllocateChesspiece("white_pawn", 0, 1), AllocateChesspiece("white_pawn", 1, 1), AllocateChesspiece("white_pawn", 2, 1),
            AllocateChesspiece("white_pawn", 3, 1), AllocateChesspiece("white_pawn", 4, 1), AllocateChesspiece("white_pawn", 5, 1),
            AllocateChesspiece("white_pawn", 6, 1), AllocateChesspiece("white_pawn", 7, 1) };
        playerBlack = new GameObject[] { AllocateChesspiece("black_rook", 0, 7), AllocateChesspiece("black_knight", 1, 7),
            AllocateChesspiece("black_bishop", 2, 7), AllocateChesspiece("black_queen", 3, 7), AllocateChesspiece("black_king", 4, 7),
            AllocateChesspiece("black_bishop", 5, 7), AllocateChesspiece("black_knight", 6, 7), AllocateChesspiece("black_rook", 7, 7),
            AllocateChesspiece("black_pawn", 0, 6), AllocateChesspiece("black_pawn", 1, 6), AllocateChesspiece("black_pawn", 2, 6),
            AllocateChesspiece("black_pawn", 3, 6), AllocateChesspiece("black_pawn", 4, 6), AllocateChesspiece("black_pawn", 5, 6),
            AllocateChesspiece("black_pawn", 6, 6), AllocateChesspiece("black_pawn", 7, 6) };

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            attackButton.SetActive(false);
            moveButton.SetActive(false);
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            foreach(GameObject tile in tiles)
            {
                tile.GetComponent<Tile>().SetNormal();
            }
            selectedPeice = null;
            selectedPeice.GetComponent<PieceHighlighter>().Deselect();
        }
    }

    //ü���� ����
    public GameObject AllocateChesspiece(string name,int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        positions[x, y].GetComponent<Tile>().chesspiece = obj;
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;//ü������ �̸��� ����
        cm.SetXBoard(x);//ü������ ���� xBoard���� ����
        cm.SetYBoard(y);//ü������ ���� yBoard���� ����
        cm.Activate(); //ü������ �̸��� ���� ��� ���� �� ��ġ�� �̵�
        return obj;
    }

    public Tile CreateBoard(int x, int y)
    {
        GameObject obj = Instantiate(tile, new Vector3(0, 0, 0), Quaternion.identity);
        Tile tl = obj.GetComponent<Tile>();
        tl.SetXBoard(x);//ü������ ���� xBoard���� ����
        tl.SetYBoard(y);//ü������ ���� yBoard���� ����
        tl.SetCoords(); //ü������ �̸��� ���� ��� ���� �� ��ġ�� �̵�
        if ((x + y) % 2 == 1)
            tl.SetColorBlack();
        return tl;
    }

    //8x8�� ������ ���� positions�� ü������ �־���
    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetYBoard()].chesspiece = obj;
    }
    //positions�� �ش� �ڸ��� ���
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y].chesspiece = null;
    }
    //�ش� positions�� ü������ ��ȯ��
    public GameObject GetPosition(int x, int y)
    {
        return positions[x,y].chesspiece;
    }
    //x,y���� ������ ������ ��������� bool������ ��ȯ
    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y>= positions.GetLength(1))
            return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else 
        {
            currentPlayer = "white";
        }
    }

    public void AttackTrigger()
    {
        selectedPeice.DestroyMovePlates();
        selectedPeice.InitiateAttackPlates();
    }

    public void moveTrigger()
    {
        selectedPeice.DestroyMovePlates();
        selectedPeice.InitiateMovePlates();
    }
}
