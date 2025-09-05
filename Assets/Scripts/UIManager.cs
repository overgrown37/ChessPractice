using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject UnitInfoPanel;
    [SerializeField] private TextMeshPro UnitHpText;

    private void Awake()
    {
        Instance = this;
        HidePanel();
    }

    public void ShowPanel(int currentHp, int maxHp)
    {
        UnitInfoPanel.SetActive(true);
        UnitHpText.text = $"HP: {currentHp} / {maxHp}";
    }

    public void HidePanel()
    {
        UnitInfoPanel.SetActive(false);
    }
}
