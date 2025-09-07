using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.HableCurve;

public class HPUIController : MonoBehaviour
{
    public static HPUIController Instance { get; private set; }

    [SerializeField] private GameObject Segment;
    [SerializeField] private List<GameObject> Segments;

    private RectTransform backgroundRect;
    private Camera mainCam;

    [SerializeField] private float worldYOffset = 0.5f;

    [SerializeField] private float visibleDuration = 2f; // 체력바 표시 시간 (초)
    private Coroutine hideCoroutine;

    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        backgroundRect = transform.parent.GetComponent<RectTransform>();
        mainCam = Camera.main;
    }

    public void ShowHealth(GameObject chesspiece)
    {
        int Hp = chesspiece.GetComponent<Chesspiece>().GetHP();
        int maxHp = chesspiece.GetComponent<Chesspiece>().GetMaxHP();
        SetHealth(maxHp, Hp);

        int xBoard = chesspiece.GetComponent<Chesspiece>().GetXBoard();
        int yBoard = chesspiece.GetComponent<Chesspiece>().GetYBoard();

        // 보드 좌표 -> 월드 좌표 변환
        float worldX = xBoard - 3.5f;
        float worldY = yBoard - 3.5f;

        worldYOffset = GameManager.instance.IsInverted() ? -0.5f : 0.5f;

        Vector3 worldPos = new Vector3(worldX, worldY + worldYOffset, 0f);

        // 월드 좌표를 전달
        MoveBackground(worldPos);

        backgroundRect.gameObject.SetActive(true);

        //// 이미 실행 중인 코루틴이 있으면 중지
        //if (hideCoroutine != null)
        //    StopCoroutine(hideCoroutine);

        //// 새 코루틴 실행
        //hideCoroutine = StartCoroutine(HideAfterDelay());

        //Debug.Log(Hp + "/" + maxHp + "(" + xBoard + ", " +yBoard+")");
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(visibleDuration);
        backgroundRect.gameObject.SetActive(false);
        hideCoroutine = null;
    }
    public void ForceHide()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);
        backgroundRect.gameObject.SetActive(false);
    }

    public void MoveBackground(Vector3 worldPosition)
    {
        if (backgroundRect != null && mainCam != null)
        {
            // 월드 -> 스크린 좌표 변환
            Vector3 screenPos = mainCam.WorldToScreenPoint(worldPosition);

            // UI 위치 갱신 (Screen Space - Overlay 기준)
            backgroundRect.position = screenPos;
        }
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
                img.color = Color.black;
        }
    }

    // 체력 감소/회복 함수
    public void UpdateHealth(int newHp)
    {
        currentHealth = Mathf.Clamp(newHp, 0, maxHealth);
        RefreshSegments();
    }
}
