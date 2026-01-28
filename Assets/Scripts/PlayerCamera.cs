using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform PlayerTransform;
    public BoxCollider2D MapBounds;
    public float SmoothSpeed = 0.5f;

    private float xMin, xMax, yMin, yMax;
    private float camOrthSize, camRatio;

    private void Start()
    {
        xMin = MapBounds.bounds.min.x;
        xMax = MapBounds.bounds.max.x;
        yMin = MapBounds.bounds.min.y;
        yMax = MapBounds.bounds.max.y;
        camOrthSize = Camera.main.orthographicSize;
        camRatio = (xMax + camOrthSize) / 2.0f;
    }

    private void FixedUpdate()
    {
        float camX = Mathf.Clamp(PlayerTransform.position.x, xMin + camRatio, xMax - camRatio);
        float camY = Mathf.Clamp(PlayerTransform.position.y, yMin + camOrthSize, yMax - camOrthSize);
        Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(camX, camY, transform.position.z), SmoothSpeed);
        transform.position = smoothPos;
    }
}
