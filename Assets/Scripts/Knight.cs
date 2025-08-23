using UnityEngine;

public class Knight : Chesspiece
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

        setMovePlate.PointMovePlate(2, 1); // 오른쪽 위
        setMovePlate.PointMovePlate(2, -1); // 오른쪽 아래
        setMovePlate.PointMovePlate(-2, 1); // 왼쪽 위
        setMovePlate.PointMovePlate(-2, -1); // 왼쪽 아래
        setMovePlate.PointMovePlate(1, 2); // 위쪽 오른쪽
        setMovePlate.PointMovePlate(1, -2); // 아래쪽 오른쪽
        setMovePlate.PointMovePlate(-1, 2); // 위쪽 왼쪽
        setMovePlate.PointMovePlate(-1, -2); // 아래쪽 왼쪽
    }
    public override void Attack()
    {
        // Pawn의 공격 로직 구현
        Debug.Log("Knight Attack");
    }
}
