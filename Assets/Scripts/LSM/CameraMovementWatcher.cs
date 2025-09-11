using UnityEngine;

public class CameraMovementWatcher : MonoBehaviour
{
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.position != lastPosition || transform.rotation != lastRotation)
        {
            // 카메라가 움직였음 → HP바 숨기기
            HideAllHPBars();

            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }
    }

    private void HideAllHPBars()
    {
        // 씬에 존재하는 모든 HPUIController 찾아서 끄기
        HPUIController[] bars = FindObjectsByType<HPUIController>(FindObjectsSortMode.None);

        foreach (var bar in bars)
        {
            bar.ForceHide();
        }
    }
}
