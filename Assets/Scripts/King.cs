using Unity.VisualScripting;
using UnityEngine;

public class King : Chesspiece
{
    private void Start()
    {
        hp = 3;
        skillCount = 3;
    }
    public override void Move()
    {
        int x = GetXBoard();
        int y = GetYBoard();
        SetMovePlate setMovePlate = GameManager.instance.GetComponent<SetMovePlate>();
        setMovePlate.GetPosition();
        setMovePlate.PointMovePlate(0, 1);
        setMovePlate.PointMovePlate(0, -1);
        setMovePlate.PointMovePlate(-1, 1);
        setMovePlate.PointMovePlate(-1, 0);
        setMovePlate.PointMovePlate(-1, -1);
        setMovePlate.PointMovePlate(1, 1);
        setMovePlate.PointMovePlate(1, 0);
        setMovePlate.PointMovePlate(1, -1);
    }
    public override void Attack()
    {
        // Pawn의 공격 로직 구현
        Debug.Log("King Attack");
    }
}
