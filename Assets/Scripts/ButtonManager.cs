using UnityEngine;

public class ButtonManager : MonoBehaviour// 버튼 관리 스크립트(체스말의 스킬 갯수에 따라 버튼 수와 배치를 다르게 하기)
{
    public GameObject attackButton;// 공격 버튼
    public GameObject moveButton;// 이동 버튼
    public GameObject skill_1Button;//스킬 버튼
    public GameObject skill_2Button;
    public GameObject skill_3Button;  

    void Start()// 초기화
    {
        // 버튼을 비활성화 상태로 시작
        DeactiveButton();
    }

    public void ActiveButton(int skillCount)// 버튼을 활성화 상태로 변경
    {
        attackButton.SetActive(true);
        moveButton.SetActive(true);
        /*if (skillCount > 2)
        {
            skillButton.SetActive(true);
        }*/
    }

    public void DeactiveButton()// 버튼을 비활성화 상태로 변경
    {
        attackButton.SetActive(false);
        moveButton.SetActive(false);
        /*skillButton.SetActive(false);*/
    }

    public void OnAttackButtonClick()// 공격 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().AttackSelectedPiece();// 선택된 체스말의 공격 함수 호출
        DeactiveButton();
    }

    public void OnMoveButtonClick()// 이동 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().MoveSelectedPiece();// 선택된 체스말의 이동 함수 호출
        DeactiveButton();
    }

    public void OnSkill_1ButtonClick()// 스킬 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().Skill_1SelectedPiece();
        DeactiveButton();
    }

    public void OnSkill_2ButtonClick()// 스킬 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().Skill_2SelectedPiece();
        DeactiveButton();
    }

    public void OnSkill_3ButtonClick()// 스킬 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().Skill_3SelectedPiece();
        DeactiveButton();
    }
}
