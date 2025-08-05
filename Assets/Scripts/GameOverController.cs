using Unity.VisualScripting;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private bool isGameOver = false;

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }
}
