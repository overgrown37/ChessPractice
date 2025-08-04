using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            move.y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            move.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            move.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            move.x += 1;

        move = move.normalized * moveSpeed * Time.deltaTime;

        transform.position += move;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }
}