using UnityEngine;

public class Chesspiece : MonoBehaviour
{
    private int xBoard = -1;//체스말의 좌표
    private int yBoard = -1;

    protected int hp;
    protected int skillCount;

    public Sprite white, black;

    public Vector3 Coords = new Vector3();//체스말의 화면상의 좌표(UI 효과를 위해 따로 추가)
    public string player;//체스 말이 어느 편인지

    public void SetCoords()// 화면상의 좌표 설정기
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

    public int GetXBoard()//x좌표 반환
    {
        return xBoard;
    }

    public int GetYBoard()//y좌표 반환
    {
        return yBoard;
    }

    public void SetXBoard(int x)//x좌표 설정
    {
        xBoard = x;
    }

    public void SetYBoard(int y)//y좌표 설정
    {
        yBoard = y;
    }
    public int GetSkillCount()
    {
        return skillCount;
    }

    public virtual void Move()
    {
        // 기본 이동 메서드, 각 체스말 클래스에서 오버라이드하여 구현
        // 예: Pawn, Knight, Bishop 등에서 각각의 이동 로직을 구현
        // 이 메서드는 SelectManager에서 호출되어야 함
    }
    public virtual void Attack() 
    {
        
    }
    public virtual void RangedAttack()
    {
        
    }
}
