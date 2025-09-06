using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.HableCurve;

public class HPUIController : MonoBehaviour
{
    [SerializeField] private GameObject Segment;
    [SerializeField] private List<GameObject> Segments;

    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        SetHealth(maxHealth, currentHealth);
    }

    // 체력 설정
    public void SetHealth(int maxHp, int currentHp)
    {
        maxHealth = maxHp;
        currentHealth = currentHp;

        RefreshSegments();
    }

    // 세그먼트 생성 (최초 1회)
    private void CreateSegments()
    {
        // 기존 세그먼트 제거
        foreach (var seg in Segments)
        {
            Destroy(seg);
        }
        Segments.Clear();

        // 새 세그먼트 생성
        for (int i = 0; i < maxHealth; i++)
        {
            var currentSegment = Instantiate(Segment, transform);
            Segments.Add(currentSegment);
        }
    }

    // 세그먼트 색 갱신
    private void RefreshSegments()
    {
        if (Segments.Count != maxHealth)
        {
            CreateSegments();
        }

        for (int i = 0; i < maxHealth; i++)
        {
            var img = Segments[i].GetComponent<Image>();
            if (i < currentHealth)
            {
                if ((float)currentHealth / (float)maxHealth > 0.5f)
                {
                    img.color = Color.green;
                }
                else if ((float)currentHealth / (float)maxHealth > 0.25f)
                {
                    img.color = Color.yellow;
                }
                else
                {
                    img.color = Color.red;
                }
            }

            else
                img.color = Color.grey;
        }
    }

    // 체력 감소/회복 함수
    public void UpdateHealth(int newHp)
    {
        currentHealth = Mathf.Clamp(newHp, 0, maxHealth);
        RefreshSegments();
    }
}
