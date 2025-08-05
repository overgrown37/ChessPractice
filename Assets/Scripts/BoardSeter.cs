using UnityEditor;
using UnityEngine;

public class BoardSeter : MonoBehaviour
{
    public GameObject tile;
    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameManager.instance.positions[i, j] = CreateBoard(i, j);
            }
        }

        SetChessPosittion();
    }

    public GameObject CreateBoard(int x, int y)
    {
        GameObject obj = Instantiate(tile, new Vector3(0, 0, 0), Quaternion.identity);
        TileCoord tl = obj.GetComponent<TileCoord>();
        TileColor tc = obj.GetComponent<TileColor>();
        tl.SetXBoard(x);
        tl.SetYBoard(y);
        tl.SetCoords();
        if ((x + y) % 2 == 1)
        {
            tc.SetColorBlack();
        }
        return obj;
    }

    private void SetChessPosittion()
    {
        GameManager.instance.playerBlack = new GameObject[] { AllocateChesspiece("rook", 0, 0), AllocateChesspiece("knight", 1, 0),
            AllocateChesspiece("bishop", 2, 0), AllocateChesspiece("queen", 3, 0), AllocateChesspiece("king", 4, 0),
            AllocateChesspiece("bishop", 5, 0), AllocateChesspiece("knight", 6, 0), AllocateChesspiece("rook", 7, 0),
            AllocateChesspiece("pawn", 0, 1), AllocateChesspiece("pawn", 1, 1), AllocateChesspiece("pawn", 2, 1),
            AllocateChesspiece("pawn", 3, 1), AllocateChesspiece("pawn", 4, 1), AllocateChesspiece("pawn", 5, 1),
            AllocateChesspiece("pawn", 6, 1), AllocateChesspiece("pawn", 7, 1) };
        GameManager.instance.playerWhite = new GameObject[] { AllocateChesspiece("rook", 0, 7), AllocateChesspiece("knight", 1, 7),
            AllocateChesspiece("bishop", 2, 7), AllocateChesspiece("queen", 3, 7), AllocateChesspiece("king", 4, 7),
            AllocateChesspiece("bishop", 5, 7), AllocateChesspiece("knight", 6, 7), AllocateChesspiece("rook", 7, 7),
            AllocateChesspiece("pawn", 0, 6), AllocateChesspiece("pawn", 1, 6), AllocateChesspiece("pawn", 2, 6),
            AllocateChesspiece("pawn", 3, 6), AllocateChesspiece("pawn", 4, 6), AllocateChesspiece("pawn", 5, 6),
            AllocateChesspiece("pawn", 6, 6), AllocateChesspiece("pawn", 7, 6) };

        foreach(GameObject piece in GameManager.instance.playerWhite)
        {
            piece.GetComponent<SpriteRenderer>().sprite = piece.GetComponent<Chesspiece>().white;
            piece.GetComponent<Chesspiece>().player = "white";
        }
        foreach (GameObject piece in GameManager.instance.playerBlack)
        {
            piece.GetComponent<SpriteRenderer>().sprite = piece.GetComponent<Chesspiece>().black;
            piece.GetComponent<Chesspiece>().player = "black";
        }
    }

    private GameObject AllocateChesspiece(string name, int x, int y)
    {
        GameObject obj = null;

        if (name == "king")
        {
            obj = Instantiate(king, new Vector3(0, 0, -1), Quaternion.identity);
        }
        else if (name == "queen")
        {
            obj = Instantiate(queen, new Vector3(0, 0, -1), Quaternion.identity);
        }
        else if (name == "rook")
        {
            obj = Instantiate(rook, new Vector3(0, 0, -1), Quaternion.identity);
        }
        else if (name == "bishop")
        {
            obj = Instantiate(bishop, new Vector3(0, 0, -1), Quaternion.identity);
        }
        else if (name == "knight")
        {
            obj = Instantiate(knight, new Vector3(0, 0, -1), Quaternion.identity);
        }
        else if (name == "pawn")
        {
            obj = Instantiate(pawn, new Vector3(0, 0, -1), Quaternion.identity);
        }

        GameManager.instance.positions[x, y].GetComponent<TileCoord>().SetChesspiece(obj);
        Chesspiece cp = obj.GetComponent<Chesspiece>();
        cp.SetXBoard(x);
        cp.SetYBoard(y);
        cp.SetCoords();
        return obj;
    }
}
