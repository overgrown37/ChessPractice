using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedPiece = null;
    [SerializeField]
    private GameObject prevSelectedPiece = null;

    public GameObject GetSelectedPiece()
    {
        return selectedPiece;
    }

    public void SetSelectedPiece(GameObject piece)
    {
        if (selectedPiece != null)
        {
            prevSelectedPiece = selectedPiece;
            prevSelectedPiece.GetComponent<PieceHighlighter>().Deselect();
        }
        selectedPiece = piece;
        selectedPiece.GetComponent<PieceHighlighter>().Select();
    }

    public void SetEmptySelectedPiece()
    {
        if (selectedPiece != null)
        {
            prevSelectedPiece = selectedPiece;
            selectedPiece = null;
            prevSelectedPiece.GetComponent<PieceHighlighter>().Deselect();
        }
    }
}
