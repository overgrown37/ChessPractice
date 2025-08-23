using UnityEngine;

public class TileColor : MonoBehaviour
{
    public Color highlightColor = Color.gray; // 하이라이트 색상
    public Color originalColor;// 원래 색상

    private void Start()
    {
        SetOriginalColor();// 오브젝트 생성시 색을 원래 색상으로 설정
    }

    public void SetColorBlack()
    {
        originalColor = Color.black; // 원래 색상을 검정색으로 설정
        gameObject.GetComponent<SpriteRenderer>().color = originalColor; // 타일의 색상을 검정색으로 설정
    }

    public void SetHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = highlightColor;// 타일의 색상을 하이라이트 색상으로 설정
    }

    public void SetOriginalColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;// 타일의 색상을 원래 색상으로 설정
    }

}
