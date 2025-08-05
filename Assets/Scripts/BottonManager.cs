using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject attackButton;
    public GameObject moveButton;

    void Start()
    {
        attackButton.SetActive(false);
        moveButton.SetActive(false);
    }
}
