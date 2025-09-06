using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject[,] positions = new GameObject[8, 8];// 보드의 타일들을 담을 배열
    public GameObject[] playerBlack;// 흑색 플레이어의 체스말들
    public GameObject[] playerWhite;// 백색 플레이어의 체스말들
    public GameObject Camera; //카메라 회전용
    public bool isInverted = false; //카메라 회전 확인용

    private void Awake()
    {
        if (instance == null)// GameManager 싱글톤 패턴 구현
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPosition(GameObject obj)// 타일에 체스말을 배치
    {
        Chesspiece cp = obj.GetComponent<Chesspiece>();

        positions[cp.GetXBoard(), cp.GetYBoard()].GetComponent<TileCoord>().SetChesspiece(obj);// 타일의 좌표를 가져와서 체스말을 배치
    }
    
    public void SetPositionEmpty(int x, int y)// 해당 좌표의 타일의 체스말을 비움
    {
        positions[x, y].GetComponent<TileCoord>().SetEmptyChesspiece();
    }

    public GameObject GetPosition(int x, int y)// 해당 좌표의 타일의 체스말을 반환
    {
        return positions[x, y].GetComponent<TileCoord>().GetChesspiece();
    }
   
    public bool PositionOnBoard(int x, int y)// 해당 좌표가 보드 내에 있는지 확인
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y>= positions.GetLength(1))
            return false;
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeView();
            if (isInverted)
            {
                isInverted = false;
            }
            else
            {
                isInverted = true;
            }
        }
    }
    public void ChangeView()
    {
        Camera.transform.Rotate(0, 0, 180);
        foreach(var c in playerBlack)
        {
            c.transform.Rotate(0, 0, 180);
        }
        foreach(var c in playerWhite)
        {
            c.transform.Rotate(0, 0, 180);
        }
    }
}
