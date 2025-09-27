using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour// 버튼 관리 스크립트(체스말의 스킬 갯수에 따라 버튼 수와 배치를 다르게 하기)
{
    private GameObject currentPiece; // activebutton 할 때 함수 인자 2개 주면 오류 발생 -> 선택된 말을 따로 가지고 옴
    public void setSelectedPiece(GameObject piece) => currentPiece = piece;

    public GameObject attackButton;// 공격 버튼
    public GameObject moveButton;// 이동 버튼

    public GameObject backButton;// 뒤로가기 버튼

    public RectTransform actionBar;// 버튼을 넣어서 정렬시키는 곳

    public List<GameObject> skillButtons = new List<GameObject>();// 스킬 버튼 리스트(캔버스에서 가져와서 리스트에 저장)
    public string iconChildname = "Icon";
    public Vector2 iconSize = new Vector2(32, 32);

    public CenterButtons center;//정렬 기능 스크립트

    void Start()// 초기화
    {
        // 버튼을 비활성화 상태로 시작
        DeactiveButton();
        DeactiveBackButton();
    }

    void Awake()
    {
        if (!actionBar) actionBar = GetComponent<RectTransform>();
        center = actionBar ? actionBar.GetComponent<CenterButtons>() : null;

        // skillButtons를 비워놨다면 ActionBar 자식 중 "skill"이름 가진 버튼 자동 수집(비활성 포함)
        if (skillButtons == null || skillButtons.Count == 0)
        {
            skillButtons = actionBar
                .GetComponentsInChildren<Button>(true)            // 버튼 컴포넌트 기준으로
                .Select(b => b.gameObject)
                .Where(go => go.name.ToLower().Contains("skill")) // 이름에 "skill" 포함
                .OrderBy(go => go.transform.GetSiblingIndex())    // Hierarchy 순서대로
                .ToList();
        }

        DeactiveButton();
    }



    public void DeactiveButton()// 버튼을 비활성화 상태로 변경
    {
        if (attackButton) attackButton.SetActive(false);
        if (moveButton) moveButton.SetActive(false);
        foreach (var s in skillButtons) if (s) s.SetActive(false);
        if (actionBar) actionBar.gameObject.SetActive(false);
    }

    public void DeactiveBackButton()// 뒤로가기 버튼 클릭 시 호출되는 함수
    {
        if(backButton) backButton.SetActive(false);
    }

    public void ActiveButton(int skillCount) // 오버로드(함수 인자 전달 오류 개선)
    {
        ActiveButton(skillCount, currentPiece);
    }

    public void ActiveBackButton()// 뒤로가기 버튼 클릭 시 호출되는 함수
    {
        if (backButton) backButton.SetActive(true);
    }

    public void ActiveButton(int skillCount, GameObject piece) // 진짜 activebutton 함수
    {
        if (!actionBar) return;

        // 고정 버튼 ON
        if (attackButton) attackButton.SetActive(true);
        if (moveButton) moveButton.SetActive(true);

        // 필요한 스킬 개수 계산(최소 0)
        int needSkills = Mathf.Max(0, skillCount - 2);

        // 현재 piece의 남은 스킬 개수 
        int[] remainedSkills = piece.GetComponent<Chesspiece>().GetRemainedSkills();

        // 스킬 버튼 토글
        int activeSkillButtonCount = 0;
        for (int i = 0; i < skillButtons.Count; i++)
        {
            bool shouldShow = false;
            bool isInCooltime = piece.GetComponent<Chesspiece>().IsInCoolTime(i);

            if (i < needSkills) // 필요한 스킬 개수 범위 내
            {
                // 스킬 개수 검사(remainedSkill 배열 범위 내이고 값이 0보다 클 때만 활성화)
                if (remainedSkills != null && i < remainedSkills.Length && remainedSkills[i] > 0)
                {
                    shouldShow = true;
                    activeSkillButtonCount++;
                }
            }

            if (skillButtons[i])
            {
                skillButtons[i].SetActive(shouldShow);

                // 쿨타임 처리 ( 버튼이 활성화되어 있을 때만 )
                if (shouldShow)
                {
                    Button btnComponent = skillButtons[i].GetComponent<Button>();
                    if (btnComponent != null)
                    {
                        btnComponent.interactable = !isInCooltime; // 쿨타임 중이면 상호작용 불가
                    }
                }
            }
        }

        ApplySkill_Icons(piece, activeSkillButtonCount, remainedSkills);

        // 바 표시 + 중앙 정렬 새로고침
        actionBar.gameObject.SetActive(true);
        if (center) center.AlignChildren();
    }

    //스킬버튼에 아이콘 적용
    private void ApplySkill_Icons(GameObject piece, int activeSkillCount, int[] remainedSkills)
    {
        int iconIndex = 0; //실제 아이콘을 적용할 인덱스

        for (int i = 0; i < skillButtons.Count; i++)
        {
            var btn = skillButtons[i];
    
            if (!btn) continue; //버튼이 null이면 건너뛰기

            // 해당 스킬이 존재하고 사용 가능한지 확인
            if (remainedSkills != null && i < remainedSkills.Length && remainedSkills[i] > 0)
            {
                // Icon 이미지 찾기
                Image icon = null;
                var t = btn.transform.Find(iconChildname);
                if (t) icon = t.GetComponent<Image>();
                if (!icon) icon = btn.GetComponentInChildren<Image>(true);

                if (icon != null)
                {
                    var sp = piece.GetComponent<Chesspiece>().GetSkill_img(i);
                    icon.sprite = sp;
                    icon.preserveAspect = true;
                    var rt = icon.rectTransform;
                    rt.sizeDelta = iconSize;
                }

                // 쿨타임 확인 및 텍스트 처리
                bool isInCooltime = piece.GetComponent<Chesspiece>().IsInCoolTime(i);
                int coolTimeRemaining = piece.GetComponent<Chesspiece>().GetCoolTimeRemaining(i);

                Debug.Log($"=== 버튼 {i} 텍스트 처리 ===");
                Debug.Log($"isInCooltime: {isInCooltime}");
                Debug.Log($"coolTimeRemaining: {coolTimeRemaining}");
                Debug.Log($"remainedSkills[{i}]: {remainedSkills[i]}");

                // 스킬 남은 개수 또는 쿨타임 텍스트 설정
                TextMeshProUGUI skillCountText = btn.GetComponentInChildren<TextMeshProUGUI>();
                Debug.Log($"skillCountText 찾기 결과: {skillCountText?.name}");

                if (skillCountText != null)
                {
                    if (isInCooltime && coolTimeRemaining > 0)
                    {
                        // 쿨타임 중이면 남은 턴 수 표시
                        skillCountText.text = coolTimeRemaining.ToString();
                        skillCountText.color = Color.red; // 쿨타임 중일 때 빨간색
                        Debug.Log($"쿨타임 텍스트 설정: {coolTimeRemaining}");
                    }
                    else
                    {
                        // 평상시엔 남은 스킬 횟수 표시
                        skillCountText.text = remainedSkills[i].ToString();
                        skillCountText.color = Color.white; // 기본 색상
                    }
                }
                else
                {
                    Debug.LogWarning($"[ButtonManager] {btn.name} 버튼에서 Text 컴포넌트를 찾을 수 없습니다!");
                }

                iconIndex++;
            }
        }
    }

    public bool IsInCoolTime(int i) // 스킬 쿨타임 확인 함수
    {
        if (currentPiece != null)
        {
            return currentPiece.GetComponent<Chesspiece>().IsInCoolTime(i); // 선택된 체스말의 쿨타임 확인 함수 호출
        }
        return false;
    }


    public void OnAttackButtonClick()// 공격 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().AttackSelectedPiece();// 선택된 체스말의 공격 함수 호출
        GameManager.instance.SetAllChesspieceCollidersEnabled(false);
        DeactiveButton();
        ActiveBackButton();
    }

    public void OnMoveButtonClick()// 이동 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().MoveSelectedPiece();// 선택된 체스말의 이동 함수 호출
        GameManager.instance.SetAllChesspieceCollidersEnabled(false);
        DeactiveButton();
        ActiveBackButton();
    }

    public void OnSkill1ButtonClick()// 스킬 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().SkillAttack1SelectedPiece();// 선택된 체스말의 스킬1 함수 호출
        GameManager.instance.SetAllChesspieceCollidersEnabled(false);
        DeactiveButton();
        ActiveBackButton();
    }

    public void OnBackButtonClick() // 뒤로가기 버튼 클릭 시 호출되는 함수
    {
        GameManager.instance.GetComponent<SelectManager>().DeleteTileState();// 타일 상태 정리
        GameManager.instance.SetAllChesspieceCollidersEnabled(true);

        GameManager.instance.GetComponent<SetRangedAttackPlate>().fanAttack = false;
        GameManager.instance.GetComponent<SetRangedAttackPlate>().roundAttack = false;
        GameManager.instance.GetComponent<SetRangedAttackPlate>().ClearRangedAttackPlates(); // 스킬 공격 타일 제거

        if (currentPiece != null) // 만약 선택된 체스말이 있다면
        {
            int skillCount = currentPiece.GetComponent<Chesspiece>().GetSkillCount();
            ActiveButton(skillCount, currentPiece); // 버튼 다시 활성화
        }

        DeactiveBackButton();
    }

}
