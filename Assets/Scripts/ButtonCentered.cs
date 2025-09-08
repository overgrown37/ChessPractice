using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CenterButtons : MonoBehaviour
{
    [SerializeField] private float spacing = 200f; // 버튼 사이 간격(px)

    void Update()
    {
        AlignChildren();
    }

    public void AlignChildren()
    {
        // 활성화된 자식 RectTransform만 모으기
        List<RectTransform> buttons = new List<RectTransform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                buttons.Add(child as RectTransform);
        }
        int count = buttons.Count;
        if (count == 0) return;

        // 전체 폭 계산
        float totalWidth = (count - 1) * spacing;

        // 첫 번째 버튼 시작 X 좌표 (중앙 기준 왼쪽으로 절반 이동)
        float startX = -totalWidth / 2f;

        // 버튼들 배치
        for (int i = 0; i < count; i++)
        {
            var btn = buttons[i];
            btn.anchoredPosition = new Vector2(startX + i * spacing, 0f);
        }
    }
}