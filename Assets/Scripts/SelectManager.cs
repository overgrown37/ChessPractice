using System.Runtime.CompilerServices;
using UnityEngine;
using static SkillDamageList;
public class SelectManager : MonoBehaviour// 선택 관리 스크립트
{
    [SerializeField]
    private GameObject selectedPiece = null;// 현재 선택된 체스말
    [SerializeField]
    private GameObject prevSelectedPiece = null;// 이전에 선택된 체스말
    [SerializeField]
    private int skillDamage = 0;// 선택된 체스말의 데미지

    public GameObject GetSelectedPiece()// 현재 선택된 체스말 반환
    {
        return selectedPiece;
    }

    public void SetSelectedPiece(GameObject piece)// 현재 선택된 체스말 설정
    {
        GameManager.instance.GetComponent<ButtonManager>().DeactiveButton();

        if (selectedPiece != null)// 이전에 선택된 체스말이 있다면
        {
            prevSelectedPiece = selectedPiece;// 이전 선택된 체스말로 저장
            prevSelectedPiece.GetComponent<PieceHighlighter>().Deselect();// 선택 해제
        }
        DeleteTileState();// 이전에 표시된 타일 상태 제거
        selectedPiece = piece;// 새로 선택된 체스말로 설정
        int skillCount = selectedPiece.GetComponent<Chesspiece>().GetSkillCount();// 스킬 개수 가져오기
        selectedPiece.GetComponent<PieceHighlighter>().Select();// 선택된 체스말 하이라이트
        GameManager.instance.GetComponent<ButtonManager>().setSelectedPiece(selectedPiece);
        GameManager.instance.GetComponent<ButtonManager>().ActiveButton(skillCount);// 버튼 활성화
    }

    public void SetEmptySelectedPiece()// 선택된 체스말을 비우기(이동 및 공격이후 호출하기 위한 것)
    {
        DeleteTileState();// 이전에 표시된 타일 상태 제거
        if (selectedPiece != null)// 선택된 체스말이 있다면
        {
            prevSelectedPiece = selectedPiece;// 이전 선택된 체스말로 저장
            selectedPiece = null;// 현재 선택된 체스말을 비우기
            prevSelectedPiece.GetComponent<PieceHighlighter>().Deselect();// 선택 해제
            GameManager.instance.GetComponent<ButtonManager>().DeactiveButton();// 버튼 비활성화
        }
    }

    public void AttackSelectedPiece()// 선택된 체스말의 공격 함수 호출
    {
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<Chesspiece>().Attack();// 공격 함수 호출
        }
    }

    public void MoveSelectedPiece()// 선택된 체스말의 이동 함수 호출
    {
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<Chesspiece>().Move();// 이동 함수 호출
        }
    }

    public void SkillAttack1SelectedPiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<Chesspiece>().SkillAttack1();// 스킬 함수 호출
        }
    }

    public void SkillAttack2SelectedPiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<Chesspiece>().SkillAttack2();// 스킬 함수 호출
        }
    }

    public void SetSkillDamageSelectedPiece(int damage)// 선택된 체스말의 데미지 함수 호출
    {
        skillDamage = damage;
    }

    public int GetSkillDamageSelectedPiece()
    {
        return skillDamage;
    }

    public void DeleteTileState()
    {
        GameManager.instance.GetComponent<SetMovePlate>().ClearMovePlates(); // 이동 가능한 타일 제거
        GameManager.instance.GetComponent<SetAttackPlate>().ClearAttackPlates(); // 공격 가능한 타일 제거
        GameManager.instance.GetComponent<SetRangedAttackPlate>().ClearRangedAttackPlates();//범위 공격 타일 제거
    }

}
