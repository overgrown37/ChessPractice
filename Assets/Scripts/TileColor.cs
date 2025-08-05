using UnityEngine;

public class TileColor : MonoBehaviour
{
    public Color highlightColor = Color.gray;
    public Color originalColor;

    private void Start()
    {
        SetOriginalColor();
    }

    public void SetColorBlack()
    {
        originalColor = Color.black;
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void SetHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = highlightColor;
    }

    public void SetOriginalColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

}
