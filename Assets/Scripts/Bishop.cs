using UnityEngine;
using static SkillDamageList;
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
        int damage = GetDamage(SkillType.BasicAttack);//기본 공격 유형으로 데미지 설정
        GameManager.instance.GetComponent<SelectManager>().SetSkillDamageSelectedPiece(damage);//스킬 데미지 설정
        int x = GetXBoard();
        int y = GetYBoard();
        SetAttackPlate setAttackPlate = GameManager.instance.GetComponent<SetAttackPlate>();
        setAttackPlate.GetPosition();
        setAttackPlate.LineAttackPlate(1, 1); // 오른쪽 위 대각선
        setAttackPlate.LineAttackPlate(1, -1); // 오른쪽 아래 대각선
        setAttackPlate.LineAttackPlate(-1, 1); // 왼쪽 위 대각선
        setAttackPlate.LineAttackPlate(-1, -1); // 왼쪽 아래 대각선
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