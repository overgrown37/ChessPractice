using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HpHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor = new Color(0.8f, 0.1f, 0.1f, 1f); // 진한 빨강

    // 총 지속시간 대신 hold와 fade를 분리
    [SerializeField] private float holdDuration = 10f; // 색이 고정으로 유지되는 시간(초)
    [SerializeField] private float fadeDuration = 10f; // 원래 색으로 페이드되는 시간(초)

    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void Hit(int damage)
    {
        var chess = GetComponent<Chesspiece>();
        int currentHp = chess.GetHP();
        int newHp = Damage(currentHp, damage);

        if (newHp <= 0)
        {
            KillChesspiece();
        }
        else
        {
            chess.SetHP(newHp);
            chess.hpUI.ShowFromDamage(this.gameObject);
        }
    }

    public int Damage(int hp, int damage)
    {
        int newHp = hp - damage;

        if (newHp > 0 && spriteRenderer != null)
        {
            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(FlashRoutine());
        }

        return newHp;
    }

    private IEnumerator FlashRoutine()
    {
        if (spriteRenderer == null) yield break;

        Color original = spriteRenderer.color;
        spriteRenderer.color = hitColor;

        if (holdDuration > 0f) yield return new WaitForSeconds(holdDuration);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(hitColor, original, Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }

        spriteRenderer.color = original;
        flashCoroutine = null;
    }

    public void KillChesspiece()
    {
        Destroy(this.gameObject);
        GetComponent<Chesspiece>().hpUI.ForceHide();
    }
}
