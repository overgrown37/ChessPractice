using Unity.VisualScripting;
using UnityEngine;
using static SkillDamageList;
public class King : Chesspiece
{
    private void Start()
    {
        hp = 5;
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
        int damage = GetDamage(SkillType.BasicAttack);
        GameManager.instance.GetComponent<SelectManager>().SetSkillDamageSelectedPiece(damage);
        int x = GetXBoard();
        int y = GetYBoard();
        SetAttackPlate setAttackPlate = GameManager.instance.GetComponent<SetAttackPlate>();
        setAttackPlate.GetPosition();
        setAttackPlate.PointAttackPlate(0, 1);
        setAttackPlate.PointAttackPlate(0, -1);
        setAttackPlate.PointAttackPlate(-1, 1);
        setAttackPlate.PointAttackPlate(-1, 0);
        setAttackPlate.PointAttackPlate(-1, -1);
        setAttackPlate.PointAttackPlate(1, 1);
        setAttackPlate.PointAttackPlate(1, 0);
        setAttackPlate.PointAttackPlate(1, -1);
    }

    public override void SkillAttack1()
    {
        int damage = GetDamage(SkillType.SpecialSkill1);
        GameManager.instance.GetComponent<SelectManager>().SetSkillDamageSelectedPiece(damage);
        SetRangedAttackPlate setRangedAttackPlate = GameManager.instance.GetComponent<SetRangedAttackPlate>();
        setRangedAttackPlate.RoundAttackPlate(1);
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
