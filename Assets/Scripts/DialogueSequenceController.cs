using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequenceController : MonoBehaviour
{
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
        StartCoroutine(ZoomAndPause(onSequenceStarted));
    }

    public void EndSequence()
    {
        StartCoroutine(UnzoomAndResume());
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



    private IEnumerator UnzoomAndResume()
    {
        float t = 0f;
        float duration = 1f;
        float startSize = mainCamera.orthographicSize;

        while (t < duration)
        {
            t += Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, originalSize, t / duration);
            yield return null;
        }

        playerScript.enabled = true;
        dialogueUI.SetActive(false);
    }
}