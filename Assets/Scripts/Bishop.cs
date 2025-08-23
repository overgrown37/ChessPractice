using UnityEngine;

public class Bishop : Chesspiece
{
    private void Start()
    {
        hp = 3;
        skillCount = 2;
    }
    public override void Move()
    {
        int x = GetXBoard();
        int y = GetYBoard();
        SetMovePlate setMovePlate = GameManager.instance.GetComponent<SetMovePlate>();
        setMovePlate.GetPosition();
        setMovePlate.LineMovePlate(1, 1); // 오른쪽 위 대각선
        setMovePlate.LineMovePlate(1, -1); // 오른쪽 아래 대각선
        setMovePlate.LineMovePlate(-1, 1); // 왼쪽 위 대각선
        setMovePlate.LineMovePlate(-1, -1); // 왼쪽 아래 대각선
    }
    public override void Attack()
    {
        // Pawn의 공격 로직 구현
        Debug.Log("Bishop Attack");
    }


}