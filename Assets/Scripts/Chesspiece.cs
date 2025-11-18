using UnityEngine;
using UnityEngine.UI;
using static SkillDamageList;
public class Chesspiece : MonoBehaviour
{
    [SerializeField] private GameObject hpUIPrefab;
    public HPUIController hpUI;//hp바 개별 부여용

    private int xBoard = 0;//체스말의 좌표
    private int yBoard = 0;

    protected int hp;
    protected int MaxHp = 5;
    protected int skillCount;

    protected int[] remainedSkills = new int[3]{ 1, 1, 1 }; // 남은 스킬 횟수
    protected int[] skillCooltime = new int[3]{ 1, 1, 1 }; // 스킬 쿨타임
    protected bool[] is_Cooltime = new bool[3] { false, false, false }; // 스킬 사용 시 쿨타임 중인지 아닌지 나타냄
    protected int[] turnWhenUsingSkill = new int[3] { -1, -1, -1 }; // 스킬 사용한 턴 기록

    [SerializeField] private Sprite[] skill_Img; //스킬 이미지 저장

    public enum SkillType
    {
        BasicAttack,
        SpecialSkill1,
        SpecialSkill2,
        // 필요에 따라 추가
    }
    [SerializeField]
    int moveDistance;//이동 거리    

    public Sprite white, black;

    public Vector3 Coords = new Vector3();//체스말의 화면상의 좌표(UI 효과를 위해 따로 추가)
    public string player;//체스 말이 어느 편인지

    private void Awake()
    {
        if (hpUIPrefab == null)
        {
            Debug.LogError("HP UI Prefab not assigned on " + gameObject.name);
            return;
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in scene!");
            return;
        }

        GameObject uiObj = Instantiate(hpUIPrefab, canvas.transform);
        hpUI = uiObj.GetComponentInChildren<HPUIController>();

        if (hpUI == null)
        {
            Debug.LogError("HPUIController not found on prefab " + hpUIPrefab.name);
        }
    }

    public void SetCoords()// 화면상의 좌표 설정기
    {
        float x = xBoard;
        float y = yBoard;

        x += -3.5f;
        y += -3.5f;

        Coords = new Vector3(x, y, -1.0f);
        this.transform.position = Coords;
    }

    public int GetXBoard()//x좌표 반환
    {
        return xBoard;
    }

    public int GetYBoard()//y좌표 반환
    {
        return yBoard;
    }

    public void SetXBoard(int x)//x좌표 설정
    {
        xBoard = x;
    }

    public void SetYBoard(int y)//y좌표 설정
    {
        yBoard = y;
    }

    public int GetHP()
    {
        return hp;
    }
    public int GetMaxHP()
    {
        return MaxHp;
    }
    public void SetHP(int hp)
    {
        this.hp = hp;
    }
    public void SetMaxHP(int maxHP)
    {
        this.MaxHp = maxHP;
    }
    public int GetSkillCount()
    {
        return skillCount;
    }

    public int[] GetRemainedSkills() //배열 반환
    {
        return remainedSkills; 
    }

    public int GetRemainedSkill(int index) //특정 남은 스킬 개수 반환
    {
        return (remainedSkills != null && index >= 0 && index < remainedSkills.Length)
               ? remainedSkills[index] : 0;
    }

    public virtual int GetDamage(SkillType skill)
    {
        // 기본값: 일반 공격
        return skill == SkillType.BasicAttack ? 1 : 2;
    public int GetMoveDistance()//이동 거리 반환
    {
        return moveDistance;
    }

    public virtual void Move()
    {
        // 기본 이동 메서드, 각 체스말 클래스에서 오버라이드하여 구현
        // 예: Pawn, Knight, Bishop 등에서 각각의 이동 로직을 구현
        // 이 메서드는 SelectManager에서 호출되어야 함
    }
    public virtual void Attack() 
    {
        
    }
    public virtual void SkillAttack1()
    {
        
    }
    public virtual void SkillAttack2()
    {

    }

    public void ShowUIHover()
    {
        hpUI.ShowFromHover(this.gameObject);
    }

    public void ShowUIDamage(int damage)
    {
        hpUI.ShowHealth(this.gameObject, damage);
    }

    public void HideUIHover()
    {
        hpUI.OnHoverExit();
    }

    public Sprite GetSkill_img(int i) // 스킬 이미지 가지고 오기
    {
        return (skill_Img != null && i >= 0 && i < skill_Img.Length) ? skill_Img[i] : null;
    }

    public Sprite[] GetSkill_img() => skill_Img; // 스킬 이미지 한번에 가져오기(index X)

    public void StartCoolTime(int i)
    {
        is_Cooltime[i] = true; // 쿨타임 시작
        turnWhenUsingSkill[i] = GameManager.instance.GetCurrentTurn(); // 스킬 시작 턴
    }

    public bool IsInCoolTime(int i)
    {
        int turn = GameManager.instance.GetCurrentTurn();

        if (turnWhenUsingSkill[i] + skillCooltime[i]*2 >= turn && is_Cooltime[i] == true) // 쿨타임 계산
        {
            return true;
        }
        else // 쿨타임 끝남
        {
            is_Cooltime[i] = false;
            turnWhenUsingSkill[i] = -1; // 초기화
            return false;
        }
    }

    public int GetCoolTimeRemaining(int i)
    {
        int turn = GameManager.instance.GetCurrentTurn();
        if (is_Cooltime[i])
        {
            return skillCooltime[i]-((turn - turnWhenUsingSkill[i])/2)+1;
        }
        return 0;
    }

    public void UseSkill(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < remainedSkills.Length)
        {
            remainedSkills[skillIndex] = Mathf.Max(0, remainedSkills[skillIndex] - 1);
        }
    }

    private void OnMouseEnter()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            TileState tileState = hit.collider.GetComponent<TileState>();
            if (tileState != null)
            {
                tileState.ForceShowGhost();
            }
        }
    }
    private void OnMouseExit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            TileState tileState = hit.collider.GetComponent<TileState>();
            if (tileState != null)
            {
                tileState.ForceHideGhost();
            }
        }
    }
}
