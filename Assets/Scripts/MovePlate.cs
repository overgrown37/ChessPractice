using UnityEngine;
using UnityEngine.Rendering;

public class MovePlate : MonoBehaviour
{
    //������ ü������ ��� = ���� ������ ü������ ���� ����
    GameObject selectedPiece = null;

    //MovePlate�� ��ġ
    int xBoard;
    int yBoard;

    //�����̸� �̵�, ���̸� ����
    public bool attack = false;

    private void Start()
    {
        //���� �����̸� �������� ��
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnMouseUp()
    {
        Chessman chessman = selectedPiece.GetComponent<Chessman>();
        if (attack)// ���� ���¿����� ������ ��ġ�� ü������ ���ֹ���
        {
            GameObject target = GameManager.instance.GetPosition(xBoard, yBoard);
            GameManager.instance.attackButton.SetActive(false);
            GameManager.instance.moveButton.SetActive(false);
            chessman.DestroyMovePlates();
            if (target == null || target.GetComponent<Chessman>().player == chessman.player) {
                GameManager.instance.NextTurn();
                GameManager.instance.selectedPeice.GetComponent<PieceHighlighter>().Deselect();
                GameManager.instance.selectedPeice = null;
                return;
            }

            Destroy(target);
            GameManager.instance.NextTurn();
            GameManager.instance.selectedPeice.GetComponent<PieceHighlighter>().Deselect();
            GameManager.instance.selectedPeice = null;
            return;
        }

        // �̵��� ü������ ���� ��ġ ����
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
    //��ǥ �ޱ�(Coords�� ��ǥ��� ��)
    public void SetCoords(int x, int y)
    {
        xBoard = x;
        yBoard = y;
    }
    //���õ� ü���� �ޱ�
    public void SetSelected(GameObject obj)
    {
        selectedPiece = obj;
    }
    //���õ� ü���� ��ȯ
    public GameObject GetSelcted()
    { 
        return selectedPiece; 
    }
}
