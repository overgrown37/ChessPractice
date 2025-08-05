using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool selected = false;

    public GameObject[,] positions = new GameObject[8, 8];
    public GameObject[] playerBlack;
    public GameObject[] playerWhite;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }

    public void SetPosition(GameObject obj)
    {
        Chesspiece cp = obj.GetComponent<Chesspiece>();

        positions[cp.GetXBoard(), cp.GetYBoard()].GetComponent<TileCoord>().SetChesspiece(obj);
    }
    
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y].GetComponent<TileCoord>().SetEmptyChesspiece();
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y].GetComponent<TileCoord>().GetChesspiece();
    }
   
    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y>= positions.GetLength(1))
            return false;
        return true;
    }
}
