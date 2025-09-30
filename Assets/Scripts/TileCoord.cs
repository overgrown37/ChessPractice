using UnityEngine;

public class TileCoord : MonoBehaviour
{
    private int xBoard = 0;// 타일 상의 좌표
    private int yBoard = 0;
    [SerializeField]
    private bool moveState = false; // 타일의 상태(클릭 시 이동, 공격, 범위 공격 구현용)
    [SerializeField]
    private bool attackState = false;
    [SerializeField]
    private bool rangedAttackState = false;

    private GameObject chesspiece = null; // 타일에 위치한 체스말

    public void SetCoords() //화면 상의 좌표로 옮기기
    {
       float x = xBoard;
       float y = yBoard;

        x += -3.5f;
        y += -3.5f;

        this.transform.position = new Vector3(x, y, 0);
    }

    public int GetXBoard()//x좌표 반환
    {
       return xBoard;
    }

    public int GetYBoard()//y좌표 반환
    {
        return yBoard;
    }

    public void SetXBoard(int x)//x좌표 수정
    {
        xBoard = x;
    }
    
    public void SetYBoard(int y)//y좌표 수정
    {
        yBoard = y;
    }

    public GameObject GetChesspiece()//타일 위의 체스말 반환
    {
        return chesspiece;
    }

    public void SetChesspiece(GameObject newChesspiece)//타일 위의 체스말 설정
    {
        chesspiece = newChesspiece;
    }

    public void SetEmptyChesspiece()//타일 위의 체스말 비우기
    {
        chesspiece = null;
    }

    public void SetMove()//이동 가능 타일로 설정
    {
        moveState = true;
        attackState = false;
        rangedAttackState = false;
    }

    public void SetAttack()//공격 가능 타일로 설정
    {
        moveState = false;
        attackState = true;
        rangedAttackState = false;
    }
        
    public void SetRangedAttack()//공격 범위 내의 타일로 설정
    {
        moveState = false;
        attackState = false;
        rangedAttackState = true;
        //GetChesspiece().GetComponent<Chesspiece>().ShowUIDamage(GameManager.instance.GetComponent<SelectManager>().GetSkillDamageSelectedPiece());
    }

    public bool IsMove()//이동 가능 타일인가?
    {
        return moveState;
    }

    public bool IsAttack()//공격 가능 타일인가?
    {
        return attackState;
    }

    public bool IsRangedAttack()//공격 범위 내의 타일인가?
    {
        return rangedAttackState;
    }

    public void InitState()// 타일 상태 해제
    {
        moveState = false;
        attackState = false;
        rangedAttackState = false;
    }
}
