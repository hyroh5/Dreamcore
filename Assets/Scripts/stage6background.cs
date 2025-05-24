using System.Collections;
using UnityEngine;

public class stage6background : MonoBehaviour
{
    public GameObject imageObject1; // 예: 정상 이미지
    public GameObject imageObject2; // 예: 손상 이미지
    public GameObject damagedimageText;
    public float switchInterval = 0.1f; // 전환 간격

    private Coroutine flashingRoutine;
    private bool isFlashing = false;

    public void StartFlashing()
    {
        if (!isFlashing)
        {
            flashingRoutine = StartCoroutine(FlashImages());
            isFlashing = true;
        }
    }

    public void StopFlashing()
    {
        if (isFlashing && flashingRoutine != null)
        {
            StopCoroutine(flashingRoutine);
            flashingRoutine = null;
            isFlashing = false;

            // 마지막으로 손상 이미지만 남기고 고정
            imageObject1.SetActive(false);
            imageObject2.SetActive(true);
            damagedimageText.SetActive(true);
        }
    }

    IEnumerator FlashImages()
    {
        while (true)
        {
            imageObject1.SetActive(true);
            imageObject2.SetActive(false);
            yield return new WaitForSeconds(switchInterval);

            imageObject1.SetActive(false);
            imageObject2.SetActive(true);
            yield return new WaitForSeconds(switchInterval);
        }
    }
}
