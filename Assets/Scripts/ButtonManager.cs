using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class ButtonManager : MonoBehaviour// 버튼 관리 스크립트(체스말의 스킬 갯수에 따라 버튼 수와 배치를 다르게 하기)
{
    private GameObject currentPiece; // activebutton 할 때 함수 인자 2개 주면 오류 발생 -> 선택된 말을 따로 가지고 옴
    public void setSelectedPiece(GameObject piece) => currentPiece = piece;

    public GameObject attackButton;// 공격 버튼
    public GameObject moveButton;// 이동 버튼

    public RectTransform actionBar;// 버튼을 넣어서 정렬시키는 곳

    public List<GameObject> skillButtons = new List<GameObject>();// 스킬 버튼 리스트(캔버스에서 가져와서 리스트에 저장)
    public string iconChildname = "Icon";
    public Vector2 iconSize = new Vector2(32, 32);

    public CenterButtons center;//정렬 기능 스크립트

    void Start()// 초기화
    {
        // 버튼을 비활성화 상태로 시작
        DeactiveButton();
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

    /*public void ActiveButton(int skillCount)// 버튼을 활성화 상태로 변경
    {
        attackButton.SetActive(true);
        moveButton.SetActive(true);
    }*/

    public void DeactiveButton()// 버튼을 비활성화 상태로 변경
    {
        if (attackButton) attackButton.SetActive(false);
        if (moveButton) moveButton.SetActive(false);
        foreach (var s in skillButtons) if (s) s.SetActive(false);
        if (actionBar) actionBar.gameObject.SetActive(false);
    }

    public void ActiveButton(int skillCount) // 오버로드(함수 인자 전달 오류 개선)
    {
        ActiveButton(skillCount, currentPiece);
    }

    public void ActiveButton(int skillCount, GameObject piece)
    {
        if (!actionBar) return;

        // 1) 고정 버튼 ON
        if (attackButton) attackButton.SetActive(true);
        if (moveButton) moveButton.SetActive(true);

        // 2) 필요한 스킬 개수 계산(최소 0)
        int needSkills = Mathf.Max(0, skillCount - 2);

        // 3) 스킬 버튼 토글
        for (int i = 0; i < skillButtons.Count; i++)
        {
            bool on = i < needSkills;
            if (skillButtons[i]) skillButtons[i].SetActive(on);
        }

        if (needSkills > skillButtons.Count)
        {
            Debug.LogWarning($"[ButtonManager] 스킬 {needSkills}개가 필요하지만 " +
                             $"{skillButtons.Count}개만 있습니다. (ActionBar에 스킬 버튼 추가하면 자동 반영됨)");
        }

        ApplySkill_Icons(piece, needSkills);

        // 4) 바 표시 + 중앙 정렬 새로고침
        actionBar.gameObject.SetActive(true);
        if (center) center.AlignChildren();
    }

    private void ApplySkill_Icons(GameObject piece, int needSkills)
    {
        for (int i = 0; i < needSkills; i++)
        {
            var btn = skillButtons[i];
            if (!btn) continue;

            // Icon 이미지 찾기 (자식 이름 기준)
            Image icon = null;
            var t = btn.transform.Find(iconChildname);
            if (t) icon = t.GetComponent<Image>();
            if (!icon) icon = btn.GetComponentInChildren<Image>(true); // 마지막 보정

            if (!icon) continue;

            // 말에서 i번째 스킬 아이콘 가져오기 (없으면 기본 아이콘)
            var sp = piece.GetComponent<Chesspiece>().GetSkill_img(i);
            icon.sprite = sp;

            // 픽셀아트 32×32
            icon.preserveAspect = true;
            var rt = icon.rectTransform;
            rt.sizeDelta = iconSize;
        }

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
