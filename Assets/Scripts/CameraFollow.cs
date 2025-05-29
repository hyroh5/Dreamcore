using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // ���� ��� (�÷��̾�)
    public Vector3 offset = new Vector3(0, 1.5f, -10f); // ī�޶� ��ġ ����
    public float smoothSpeed = 5f;           // �ε巯�� �̵� �ӵ�

    public float zoomLevel = 3.5f;           // ī�޶� ���� ����
    public float zoomSmoothSpeed = 3f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic)
        {
            cam.orthographicSize = zoomLevel; // �ٷ� ����
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. ��ġ ���󰡱�
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // 2. �� (������ zoomLevel ����, ���� �� ��/�ƿ� ����)
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomLevel, Time.deltaTime * zoomSmoothSpeed);
        }
    }
}
