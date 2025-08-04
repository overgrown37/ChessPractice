using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject movePlate;
    //���� �� ��ǥ
    private int xBoard = -1;
    private int yBoard = -1;
    public Vector3 Coords = new Vector3();
    public string player;

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    private void OnMouseUp()
    {
        if(!GameManager.instance.IsGameOver() && GameManager.instance.GetCurrentPlayer() == player)
        {
            if (GameManager.instance.selectedPeice != null)
                GameManager.instance.selectedPeice.GetComponent<PieceHighlighter>().Deselect();
            GameManager.instance.attackButton.SetActive(true);
            GameManager.instance.moveButton.SetActive(true);
            GameManager.instance.selectedPeice = gameObject.GetComponent<Chessman>();
            GetComponent<PieceHighlighter>().Select();
            DestroyMovePlates();
            //InitiateMovePlates();
        }
    }

    //���� Ȱ��ȭ �� ü���� ��������Ʈ ���� �� ��ġ
    public void Activate()
    {
        SetCoords();

        switch(this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black";  break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    //ü������ ������ ��ġ�� ������ ��
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.37f;
        y *= 0.37f;

        x += -1.3f;
        y += -1.3f;

        Coords = new Vector3(x, y, -1.0f);
        this.transform.position = Coords;
    }

    public int GetXBoard()//x��ǥ ��ȯ
    {
        return xBoard;
    }

    public int GetYBoard()//y��ǥ ��ȯ
    {
        return yBoard;
    }

    public void SetXBoard(int x)//x��ǥ ��ȯ
    {
        xBoard = x;
    }

    public void SetYBoard(int y)//y��ǥ ��ȯ
    {
        yBoard = y; 
    }

    public void DestroyMovePlates()//MovePlate ����
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Tile>().SetNormal();
        }
    }

    public void InitiateMovePlates()//MovePlate ����
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                break;
            case "black_knight":
            case "white_knight":
                KnightMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                KingMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void InitiateAttackPlates()//AttackPlate생성
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineAttackPlate(1, 0);
                LineAttackPlate(0, 1);
                LineAttackPlate(1, 1);
                LineAttackPlate(-1, 0);
                LineAttackPlate(0, -1);
                LineAttackPlate(-1, -1);
                LineAttackPlate(1, -1);
                LineAttackPlate(-1, 1);
                break;
            case "black_knight":
            case "white_knight":
                KnightAttackPlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineAttackPlate(1, 1);
                LineAttackPlate(1, -1);
                LineAttackPlate(-1, 1);
                LineAttackPlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                KingAttackPlate();
                break;
            case "black_rook":
            case "white_rook":
                LineAttackPlate(1, 0);
                LineAttackPlate(0, 1);
                LineAttackPlate(-1, 0);
                LineAttackPlate(0, -1);
                break;
            case "black_pawn":
                PawnAttackPlate();
                break;
            case "white_pawn":
                PawnAttackPlate();
                break;
        }
    }

    public void PointMovePlate(int x, int y)//�� �̵�
    {
        if(GameManager.instance.PositionOnBoard(x,y))
        {
            GameObject cp = GameManager.instance.GetPosition(x,y);

            if(cp == null)
            {
                CreateMovePlate(x, y);
            }
            /*
            else if(cp.GetComponent<Chessman>().player != player)
            {
                CreateAttackMovePlate(x, y);
            }
            */
        }
    }

    public void PointAttackPlate(int x, int y)//�� �̵�
    {
        if (GameManager.instance.PositionOnBoard(x, y))
        {
            GameObject cp = GameManager.instance.GetPosition(x, y);
            CreateAttackPlate(x, y);
        }
    }


    public void LineMovePlate(int xIncrement, int yIncrement)//�� �̵�(��ΰ� ������ ������)
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while(GameManager.instance.PositionOnBoard(x,y) &&
            GameManager.instance.GetPosition(x,y) == null)//���� ���̰� ��������� ��� MovePlate ����
        {
            CreateMovePlate(x, y);
            x += xIncrement;
            y += yIncrement;
        }
    }

    public void LineAttackPlate(int xIncrement, int yIncrement)//�� �̵�(��ΰ� ������ ������)
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (GameManager.instance.PositionOnBoard(x, y) &&
            GameManager.instance.GetPosition(x, y) == null)//���� ���̰� ��������� ��� MovePlate ����
        {
            x += xIncrement;
            y += yIncrement;
        }
        if(GameManager.instance.PositionOnBoard(x,y) && 
            GameManager.instance.GetPosition(x,y).GetComponent<Chessman>().player != player)//��� ü������ ������ AttackMovePlate ����
        {
            CreateAttackPlate(x, y);
        }
    }

    public void KnightMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }
    public void KnightAttackPlate()
    {
        PointAttackPlate(xBoard + 1, yBoard + 2);
        PointAttackPlate(xBoard - 1, yBoard + 2);
        PointAttackPlate(xBoard + 2, yBoard + 1);
        PointAttackPlate(xBoard + 2, yBoard - 1);
        PointAttackPlate(xBoard + 1, yBoard - 2);
        PointAttackPlate(xBoard - 1, yBoard - 2);
        PointAttackPlate(xBoard - 2, yBoard + 1);
        PointAttackPlate(xBoard - 2, yBoard - 1);
    }

    public void KingMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard - 1);
    }
    public void KingAttackPlate()
    {
        PointAttackPlate(xBoard, yBoard + 1);
        PointAttackPlate(xBoard, yBoard - 1);
        PointAttackPlate(xBoard - 1, yBoard + 1);
        PointAttackPlate(xBoard - 1, yBoard);
        PointAttackPlate(xBoard - 1, yBoard - 1);
        PointAttackPlate(xBoard + 1, yBoard + 1);
        PointAttackPlate(xBoard + 1, yBoard);
        PointAttackPlate(xBoard + 1, yBoard - 1);
    }
    public void PawnAttackPlate()
    {
        PointAttackPlate(xBoard, yBoard + 1);
        PointAttackPlate(xBoard, yBoard - 1);
        PointAttackPlate(xBoard - 1, yBoard + 1);
        PointAttackPlate(xBoard - 1, yBoard);
        PointAttackPlate(xBoard - 1, yBoard - 1);
        PointAttackPlate(xBoard + 1, yBoard + 1);
        PointAttackPlate(xBoard + 1, yBoard);
        PointAttackPlate(xBoard + 1, yBoard - 1);
    }

    public void PawnMovePlate(int x, int y)
    {
        if(GameManager.instance.PositionOnBoard(x,y))
        {
            if(GameManager.instance.GetPosition(x,y) == null)
            {
                CreateMovePlate(x, y);
            }
        }
    }

    // �̵� Ÿ�� �����
    public void CreateMovePlate(int matrixX, int matrixY)
    {
        /*
        float x = matrixX;
        float y = matrixY;

        x *= 0.37f;
        y *= 0.37f;

        x += -1.3f;
        y += -1.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
     
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetSelected(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
        */

        Tile tile = GameManager.instance.positions[matrixX, matrixY];
        tile.selectedPiece = gameObject;
        tile.SetMove();
    }

    public void CreateAttackPlate(int matrixX, int matrixY)
    {
        /*
        float x = matrixX;
        float y = matrixY;

        x *= 0.37f;
        y *= 0.37f;

        x += -1.3f;
        y += -1.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetSelected(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
        */

        Tile tile = GameManager.instance.positions[matrixX, matrixY];
        tile.selectedPiece = gameObject;
        tile.SetAttack();
    }
}
