using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    //���� �� ��ǥ
    private int xBoard = -1;
    private int yBoard = -1;
    private bool move = false;
    private bool attack = false;

    public Color highlightColor = Color.gray;
    public Color originalColor;
    public GameObject chesspiece = null;
    public GameObject selectedPiece = null;

    private void Awake()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnMouseUp()
    {
        if (move) 
        {
            Chessman chessman = selectedPiece.GetComponent<Chessman>();
            GameManager.instance.SetPositionEmpty(chessman.GetXBoard(), chessman.GetYBoard());
            GameManager.instance.attackButton.SetActive(false);
            GameManager.instance.moveButton.SetActive(false);
            // ü������ �ش� MovePlate �ڸ��� �̵�
            chessman.SetXBoard(xBoard);
            chessman.SetYBoard(yBoard);
            chessman.SetCoords();
            //8x8 positions�� ������ ü���� �ֱ�
            GameManager.instance.SetPosition(selectedPiece);
            //�� �ѱ��
            GameManager.instance.NextTurn();
            //�̵��� ������ MovePlate�� ����
            chessman.DestroyMovePlates();
            GameManager.instance.selectedPeice.GetComponent<PieceHighlighter>().Deselect();
            GameManager.instance.selectedPeice = null;
        }

        if (attack)// ���� ���¿����� ������ ��ġ�� ü������ ���ֹ���
        {
            Chessman chessman = selectedPiece.GetComponent<Chessman>();
            GameObject target = GameManager.instance.GetPosition(xBoard, yBoard);
            GameManager.instance.attackButton.SetActive(false);
            GameManager.instance.moveButton.SetActive(false);
            chessman.DestroyMovePlates();
            if (target == null || target.GetComponent<Chessman>().player == chessman.player)
            {
                GameManager.instance.NextTurn();
                selectedPiece.GetComponent<PieceHighlighter>().Deselect();
                selectedPiece = null;
                return;
            }

            Destroy(target);
            GameManager.instance.NextTurn();
            selectedPiece.GetComponent<PieceHighlighter>().Deselect();
            selectedPiece = null;
            return;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.37f;
        y *= 0.37f;

        x += -1.3f;
        y += -1.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
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

    public void SetColorBlack()
    {
        originalColor = Color.black;
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void SetHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = highlightColor;
    }

    public void SetOriginalColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void SetAttack()
    {
        attack = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void SetMove()
    {
        move = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void SetNormal()
    {
        attack = false;
        move = false;
        selectedPiece = null;
        SetOriginalColor();
    }
}
