using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    public GameObject[,] positions = new GameObject[8, 8];// 보드의 타일들을 담을 배열
    public GameObject[] playerBlack;// 흑색 플레이어의 체스말들
    public GameObject[] playerWhite;// 백색 플레이어의 체스말들
    public GameObject Camera; //카메라 회전용
    public bool isInverted = false; //카메라 회전 확인용
    [SerializeField] private Next_turn_UI turnBanner;

    public int turn = 1;

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
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public bool IsGameOver { get; private set; } = false;
    public void GameOver(Chesspiece deadKing)
    {
        if (IsGameOver) return;
        IsGameOver = true;

        string winner = (deadKing.player == "white") ? "Black" : "White";

        // UI 컨트롤러가 비활성화여도 찾아서 켜서 보여줌
        var ui = FindObjectOfType<GameOverController>(true);
        if (ui != null) ui.Show(winner, "King destroyed");
    }
    public void ClearGameOverFlag()
    {
        IsGameOver = false;
        var gameOverController = FindObjectOfType<GameOverController>();
        if (gameOverController != null)
            gameOverController.SetGameOver(false);
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
            
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            MultiAttackCheck();

        }
    }
    
    public void ChangeView()
    {
        StartCoroutine(ChangeViewRoutine());
    }

    private IEnumerator ChangeViewRoutine()
    {
        if (turnBanner != null)
            yield return StartCoroutine(turnBanner.ShowAndHide());

        // 2) 배너가 내려간 뒤 회전 시작
        yield return new WaitForSeconds(0.1f); // (선택) 아주 짧은 텀

        Camera.transform.DORotate(new Vector3(0, 0, 180), 1f, RotateMode.WorldAxisAdd);

        GameObject[] EveryPiece = GameObject.FindGameObjectsWithTag("Chesspiece");
        foreach (var piece in EveryPiece)
            piece.transform.DORotate(new Vector3(0, 0, 180), 1f, RotateMode.WorldAxisAdd);

        turn++;  // 턴 계산

        isInverted = !isInverted;
    }
    public void MultiAttackCheck()
    {
        GameObject[] EveryPiece = GameObject.FindGameObjectsWithTag("Chesspiece");
        foreach( var c in EveryPiece)
        {
            c.GetComponent<HpHandler>().Hit(1);
        }
    }

    public bool IsInverted()
    {
        return isInverted;
    }

    public void SetAllChesspieceCollidersEnabled(bool enabled)
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Chesspiece");
        foreach (var piece in pieces)
        {
            BoxCollider2D col = piece.GetComponent<BoxCollider2D>();
            if (col != null)
                col.enabled = enabled;
        }
    }

    public int GetCurrentTurn()
    {
        return turn;
    }

}
