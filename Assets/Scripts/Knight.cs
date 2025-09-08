using UnityEngine;
using static SkillDamageList;
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
        int damage = GetDamage(SkillType.BasicAttack);
        SetAttackPlate setAttackPlate = GameManager.instance.GetComponent<SetAttackPlate>();
        setAttackPlate.GetPosition();
        setAttackPlate.PointAttackPlate(2, 1); // 오른쪽 위
        setAttackPlate.PointAttackPlate(2, -1); // 오른쪽 아래
        setAttackPlate.PointAttackPlate(-2, 1); // 왼쪽 위
        setAttackPlate.PointAttackPlate(-2, -1); // 왼쪽 아래
        setAttackPlate.PointAttackPlate(1, 2); // 위쪽 오른쪽
        setAttackPlate.PointAttackPlate(1, -2); // 아래쪽 오른쪽
        setAttackPlate.PointAttackPlate(-1, 2); // 위쪽 왼쪽
        setAttackPlate.PointAttackPlate(-1, -2); // 아래쪽 왼쪽
        GameManager.instance.GetComponent<SelectManager>().SetSkillDamageSelectedPiece(damage);
    }

    public override int GetDamage(SkillType skill)
    {
        switch (skill)
        {
            case SkillType.BasicAttack:
                return 1;
            case SkillType.SpecialSkill1:
                return 2;
            default:
                return 1;
        }
    }
}
