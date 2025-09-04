using UnityEngine;
using static SkillDamageList;
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
        int damage = GetDamage(SkillType.BasicAttack);
        GameManager.instance.GetComponent<SelectManager>().SetSkillDamageSelectedPiece(damage);
        int x = GetXBoard();
        int y = GetYBoard();
        SetAttackPlate setAttackPlate = GameManager.instance.GetComponent<SetAttackPlate>();
        setAttackPlate.GetPosition();
        setAttackPlate.LineAttackPlate(1, 0); // 오른쪽
        setAttackPlate.LineAttackPlate(-1, 0); // 왼쪽
        setAttackPlate.LineAttackPlate(0, 1); // 위쪽
        setAttackPlate.LineAttackPlate(0, -1); // 아래쪽
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