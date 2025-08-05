using UnityEngine;

public class ButtonManaer : MonoBehaviour
{
    public GameObject attackButton;
    public GameObject moveButton;

    void Start()
    {
        attackButton.SetActive(false);
        moveButton.SetActive(false);
    }
}
