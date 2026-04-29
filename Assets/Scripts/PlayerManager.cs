using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string player = "black";

    public string GetPlayer()
    {
        return player;
    }

    public void NextPlayer()
    {
        if (player == "black")
        {
            Debug.Log("Next Player: White");
            player = "white";

        }
        else
        {
            Debug.Log("Next Player: Black");
            player = "black";
        }
        GetComponent<GameManager>().ChangeView(); // 턴 바뀔 때 카메라 회전
        gameObject.GetComponent<ButtonManager>().DeactiveBackButton(); // 턴 바뀔 때 돌아가기 버튼 비활성화
    }
}