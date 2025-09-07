using UnityEngine;

public class CursorManage : MonoBehaviour// 커서 관리 스크립트 여기다 If문으로 상호작용할 수 있는 오브젝트만 바뀌게 만들면 더 좋다.
{
    public Texture2D hand;// 커서로 사용할 손 이미지
    public Texture2D original;// 원래 커서 이미지

    public void OnMouseOver()// 마우스가 오브젝트 위에 있을 때 호출되는 함수
    {
        Cursor.SetCursor(hand, new Vector2(0, 0), CursorMode.Auto); // 손 이미지로 커서 변경
    }

    public void OnMouseExit()// 마우스가 오브젝트에서 벗어날 때 호출되는 함수
    {
        Cursor.SetCursor(original, new Vector2(0, 0), CursorMode.Auto);// 원래 커서 이미지로 변경
        HPUIController.Instance.ForceHide();
    }
}
