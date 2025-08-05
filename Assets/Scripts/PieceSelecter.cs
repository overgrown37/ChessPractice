using UnityEngine;

public class PieceSelecter : MonoBehaviour
{
    private void OnMouseUp()
    {
        if (!GameManager.instance.GetComponent<GameOverController>().IsGameOver() //게임 오버 되었는지
            &&
            GameManager.instance.GetComponent<PlayerManager>().GetPlayer() == gameObject.GetComponent<Chesspiece>().player)//현재 플레이어가 선택한 체스말과 같은 편인지
        {
            GameManager.instance.GetComponent<SelectManager>().SetSelectedPiece(gameObject);
        }
    }
}
