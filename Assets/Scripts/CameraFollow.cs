using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // 따라갈 대상 (플레이어)
    public Vector3 offset = new Vector3(0, 1.5f, -10f); // 카메라 위치 보정
    public float smoothSpeed = 5f;           // 부드러운 이동 속도

    public float zoomLevel = 3.5f;           // 카메라 줌인 정도
    public float zoomSmoothSpeed = 3f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic)
        {
            cam.orthographicSize = zoomLevel; // 바로 줌인
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. 위치 따라가기
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // 2. 줌 (정해진 zoomLevel 유지, 추후 줌 인/아웃 가능)
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomLevel, Time.deltaTime * zoomSmoothSpeed);
        }
    }
}
