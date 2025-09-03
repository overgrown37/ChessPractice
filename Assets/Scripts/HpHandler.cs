using UnityEngine;

public class HpHandler : MonoBehaviour
{
    public void Hit(int damage)
    {
        int currentHp = GetComponent<Chesspiece>().GetHP();
        currentHp = Damage(currentHp, damage);
        if (currentHp <= 0) {
            KillChesspiece();
        }
        else {
            GetComponent<Chesspiece>().SetHP(currentHp);
        }
    }

    public int Damage(int hp, int damage)
    {
        hp -= damage;
        //여기에 피격 이펙트
        return hp;
    }

    public void KillChesspiece()
    {
        //여기에 죽는 이펙트
        Destroy(this.gameObject);
    }
}
