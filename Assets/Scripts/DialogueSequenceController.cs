using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequenceController : MonoBehaviour
{
    private Vector3 initialCameraPos;
    private float initialCameraSize;

    public static DialogueSequenceController Instance;

    public Camera mainCamera;
    public float zoomSize = 2.5f;
    private float originalSize;

    public GameObject dialogueUI;
    public PlayerMove playerScript;
    public Animator playerAnimator;

    void Awake()
    {
        if (Instance == null) Instance = this;
        originalSize = mainCamera.orthographicSize;
    }

    public void StartSequence(System.Action onSequenceStarted = null)
    {
        // 카메라 초기 위치와 크기 저장
        initialCameraPos = mainCamera.transform.position;
        initialCameraSize = mainCamera.orthographicSize;

        StartCoroutine(ZoomAndPause(onSequenceStarted));
    }


    [SerializeField] private float cameraYOffset = 1.5f;  // 대사를 띄우기 위한 카메라 위쪽 오프셋

    private IEnumerator ZoomAndPause(System.Action onDone = null)
    {
        playerScript.enabled = false;

        playerAnimator.SetBool("isWalking", false);

        float t = 0f;
        float duration = 1f;

        float startSize = mainCamera.orthographicSize;
        float targetSize = zoomSize;

        Vector3 startPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3(
            playerScript.transform.position.x,
            playerScript.transform.position.y + cameraYOffset, // 여기 주목
            startPos.z
        );

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerpFactor = t / duration;

            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, lerpFactor);
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, lerpFactor);

            yield return null;
        }

        dialogueUI.SetActive(true);
        onDone?.Invoke();
    }


    public void EndSequence()
    {
        // 카메라 줌 복귀
        StartCoroutine(ZoomOutAndResume());
    }

    private IEnumerator ZoomOutAndResume()
    {
        float t = 0f;
        float duration = 1f;

        float startSize = mainCamera.orthographicSize;
        Vector3 startPos = mainCamera.transform.position;

        while (t < duration)
        {
            t += Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, initialCameraSize, t / duration);
            mainCamera.transform.position = Vector3.Lerp(startPos, initialCameraPos, t / duration);
            yield return null;
        }

        dialogueUI.SetActive(false);         // 말풍선 UI 끄기
        playerScript.enabled = true;         // 이동 다시 허용
    }

}