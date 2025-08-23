using UnityEngine;

public class Rook : Chesspiece
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
        setMovePlate.LineMovePlate(1, 0); // 오른쪽
        setMovePlate.LineMovePlate(-1, 0); // 왼쪽
        setMovePlate.LineMovePlate(0, 1); // 위쪽
        setMovePlate.LineMovePlate(0, -1); // 아래쪽
    }
    public override void Attack()
    {
        // Pawn의 공격 로직 구현
        Debug.Log("Rook Attack");
    }
}