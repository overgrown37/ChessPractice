using UnityEngine;

public class TileState : MonoBehaviour// 타일 상태 관리 스크립트
{
    public enum TileType
    {
        None = 1,
        Hill = 2,
        AnotherChesspiece = 99
    }

    private TileCoord tileCoord;// 타일 좌표를 관리하는 스크립트
    private GameObject ghostPieceInstance; //잔상용
    [SerializeField]
    TileType tileType;

    void Start()
    {
        tileCoord = gameObject.GetComponent<TileCoord>();
    }

    public TileType GetTileType()
    {
        return tileType;
    }

    public int GetMoveCost()
    {
        return (int)tileType;
    }

    public void SetTileType(TileType newType)
    {
        tileType = newType;
    }

    public void SetTileType()
    {
        TileCoord tc = gameObject.GetComponent<TileCoord>();
        GameObject chesspiece = tc.GetChesspiece();
        if (chesspiece != null)
        {
            tileType = TileType.AnotherChesspiece;
        }
        else
        {
            tileType = TileType.None; // 기본값 설정
        }
    }

    private void OnMouseUp()// 마우스 클릭 시 호출되는 함수
    {
        // UI 위에서 클릭된 경우 입력 무시
        if (UnityEngine.EventSystems.EventSystem.current != null && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (tileCoord.IsMove())// 타일이 이동 가능한 상태인지 확인
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {
                Chesspiece cp = selectedPiece.GetComponent<Chesspiece>();// 선택된 체스말의 컴포넌트를 가져옴
                int prevX = cp.GetXBoard();// 선택된 체스말의 이전 X 좌표
                int prevY = cp.GetYBoard();// 선택된 체스말의 이전 Y 좌표

                GameManager.instance.SetPositionEmpty(prevX, prevY); // 1. 원래 위치 비우기
                GameManager.instance.positions[prevX, prevY].GetComponent<TileState>().SetTileType(TileType.None);// 이전 타일 상태 갱신

                cp.SetXBoard(tileCoord.GetXBoard()); // 2. 새 좌표로 변경
                cp.SetYBoard(tileCoord.GetYBoard());
                tileType = TileType.AnotherChesspiece;// 타일 상태 갱신

                GameManager.instance.SetPosition(selectedPiece);     // 3. 새 위치에 배치
                cp.SetCoords();                                     // 4. 화면상의 위치 갱신
                GameManager.instance.SetAllChesspieceCollidersEnabled(true);
                GameManager.instance.GetComponent<SelectManager>().SetEmptySelectedPiece(); // 5. 선택된 말 비우기
                GameManager.instance.GetComponent<PlayerManager>().NextPlayer(); // 6. 다음 플레이어로 전환
            }
        }
        else if (tileCoord.IsAttack())// 타일이 공격 가능한 상태인지 확인
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {
                if (tileCoord.GetChesspiece() != null)
                {
                    GameObject targetPiece = tileCoord.GetChesspiece();
                    Chesspiece targetCp = targetPiece.GetComponent<Chesspiece>();// 공격당하는 체스말의 컴포넌트
                    Chesspiece attackerCp = selectedPiece.GetComponent<Chesspiece>();// 공격하는 체스말의 컴포넌트
                    int damage = GameManager.instance.GetComponent<SelectManager>().GetSkillDamageSelectedPiece();// 선택된 체스말이 사용한 스킬을 데미지를 가져온다.
                    targetCp.GetComponent<HpHandler>().Hit(damage); // 체스말 피격 처리

                    GameManager.instance.SetAllChesspieceCollidersEnabled(true);
                    GameManager.instance.GetComponent<SetAttackPlate>().ClearAttackPlates();
                    GameManager.instance.GetComponent<SelectManager>().SetEmptySelectedPiece(); // 선택된 말 비우기
                    GameManager.instance.GetComponent<PlayerManager>().NextPlayer(); // 다음 플레이어로 전환
                    GameManager.instance.isAttacking = false;
                }
                else
                {
                    Debug.Log("공격할 체스말이 없습니다.");
                }
            }
            else
            {
                Debug.Log("선택된 체스말이 없습니다.");
            }
        }
        else if (tileCoord.IsRangedAttack())// 타일이 범위 공격 가능한 상태인지 확인
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {
                    Chesspiece attackerCp = selectedPiece.GetComponent<Chesspiece>();// 공격하는 체스말의 컴포넌트
                    int damage = GameManager.instance.GetComponent<SelectManager>().GetSkillDamageSelectedPiece();// 선택된 체스말이 사용한 스킬을 데미지를 가져온다.

                    GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Tile");
                    foreach (GameObject tile in Tiles)
                    {
                        TileCoord coord = tile.GetComponent<TileCoord>();
                        if (coord.IsRangedAttack())
                        {
                            GameObject targetPiece = coord.GetChesspiece(); // ← 각 타일의 기물로 변경
                            if (targetPiece != null)
                            {
                                Chesspiece targetCp = targetPiece.GetComponent<Chesspiece>();
                                targetCp.GetComponent<HpHandler>().Hit(damage);
                            }
                        }
                    }

                int currentSkillType = GameManager.instance.GetComponent<SelectManager>().GetCurrentSkillType();

                attackerCp.GetComponent<Chesspiece>().StartCoolTime(currentSkillType); // 스킬 쿨타임 시작
                attackerCp.UseSkill(currentSkillType); // 스킬 개수 감소

                if (GameManager.instance.GetComponent<SetRangedAttackPlate>().fanAttack == true)
                        GameManager.instance.GetComponent<SetRangedAttackPlate>().fanAttack = false;
                    if (GameManager.instance.GetComponent<SetRangedAttackPlate>().roundAttack == true)
                        GameManager.instance.GetComponent<SetRangedAttackPlate>().roundAttack = false;
                    GameManager.instance.SetAllChesspieceCollidersEnabled(true);
                    GameManager.instance.GetComponent<SetRangedAttackPlate>().ClearRangedAttackPlates();
                    GameManager.instance.GetComponent<SelectManager>().SetEmptySelectedPiece(); // 선택된 말 비우기
                    GameManager.instance.GetComponent<PlayerManager>().NextPlayer(); // 다음 플레이어로 전환
                    GameManager.instance.isAttacking = false;
            }
            else
            {
                Debug.Log("선택된 체스말이 없습니다.");
            }
        }
    
    }
    private void OnMouseEnter()
    {
        if (UnityEngine.EventSystems.EventSystem.current != null && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (tileCoord.IsMove())
        {
            GameObject selectedPiece = GameManager.instance.GetComponent<SelectManager>().GetSelectedPiece();
            if (selectedPiece != null)
            {
                // 잔상 오브젝트 생성
                ghostPieceInstance = new GameObject("GhostPiece");
                ghostPieceInstance.transform.position = transform.position;
                ghostPieceInstance.transform.localScale = selectedPiece.transform.localScale;
                ghostPieceInstance.transform.rotation = selectedPiece.transform.rotation; //rotation 복사
                // SpriteRenderer 복사
                SpriteRenderer srcRenderer = selectedPiece.GetComponent<SpriteRenderer>();
                SpriteRenderer ghostRenderer = ghostPieceInstance.AddComponent<SpriteRenderer>();
                ghostRenderer.sprite = srcRenderer.sprite;
                ghostRenderer.sortingLayerID = srcRenderer.sortingLayerID;
                ghostRenderer.sortingOrder = srcRenderer.sortingOrder + 1;
                Color c = srcRenderer.color;
                c.a = 0.5f;
                ghostRenderer.color = c;
            }
        }
    }
    private void OnMouseExit()
    {
        if (ghostPieceInstance != null)
        {
            Destroy(ghostPieceInstance);
            ghostPieceInstance = null;
        }
    }
    public void ForceShowGhost()
    {
        if (ghostPieceInstance == null)
            OnMouseEnter();
    }
    public void ForceHideGhost()
    {
        if (ghostPieceInstance != null)
            OnMouseExit();
    }
}
