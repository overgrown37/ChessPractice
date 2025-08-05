using UnityEngine;

public class CursorManage : MonoBehaviour
{
    public Texture2D hand;
    public Texture2D original;

    public void OnMouseOver()
    {
        Cursor.SetCursor(hand, new Vector2(hand.width / 3, 0), CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(original, new Vector2(0, 0), CursorMode.Auto);
    }
}
