using System.Collections;
using UnityEngine;
using TMPro;

public class MonologueTrigger7 : MonoBehaviour
{
    [Header("Trigger 조건")]
    public Transform player; // 누구 머리위로 말풍선이 따라 이동하는지 결정
    public GameObject monologueCanvas;
    public bool triggerOnce = true;

    [Header("대사 설정")]
    [TextArea(2, 2)]
    public string monologueText = "여기에 독백 대사 치삼";
    public float typingSpeed = 0.05f;

    [Header("말풍선 프리팹")]
    public GameObject speechBubblePrefab;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public Vector2 bubbleSize = new Vector2(300, 100);
    public float displayDuration = 3f;

    [Header("필수 참조")]
    public Transform canvasTransform; // Inspector에서 직접 할당

    public bool hasTriggered = false;

    public void TriggerMonologue(string content)
    {
        if (triggerOnce && hasTriggered)
        {
            Debug.Log("TriggerMonologue 중복 실행 방지됨");
            return;
        }

        if (canvasTransform == null)
        {
            Debug.LogError("canvasTransform이 Inspector에서 할당되지 않았습니다.");
            return;
        }

        if (monologueCanvas == null)
        {
            Debug.LogError("monologueCanvas가 비어 있습니다.");
            return;
        }

        if (speechBubblePrefab == null)
        {
            Debug.LogError("speechBubblePrefab이 할당되지 않았습니다.");
            return;
        }

        monologueText = content;
        hasTriggered = true;

        // Canvas 다시 활성화
        if (!monologueCanvas.activeSelf)
        {
            monologueCanvas.SetActive(true);
            Debug.Log("monologueCanvas를 다시 활성화함");
        }

        Debug.Log("▶ TriggerMonologue 실행됨: " + monologueText);
        StartCoroutine(ShowSpeechBubble());
    }

    IEnumerator ShowSpeechBubble()
    {
        GameObject bubble = Instantiate(speechBubblePrefab, player.position + offset, Quaternion.identity, canvasTransform);

        RectTransform rect = bubble.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.sizeDelta = bubbleSize;
        }

        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>();
        if (text == null)
        {
            Debug.LogError("TMP_Text 컴포넌트를 찾을 수 없습니다.");
            yield break;
        }

        text.text = "";

        yield return null; // 레이아웃 초기화 대기

        bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);

        foreach (char c in monologueText)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
            bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
        }

        float timer = 0f;
        while (timer < displayDuration)
        {
            timer += Time.deltaTime;
            bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
            yield return null;
        }

        Destroy(bubble);

        if (monologueCanvas != null)
            monologueCanvas.SetActive(false);
    }
}
