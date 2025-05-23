using System.Collections;
using UnityEngine;
using TMPro;

public class MonologueTrigger : MonoBehaviour
{
    [Header("Trigger 조건")]
    public Transform player; // 누구 머리위로 말풍선이 따라 이동하는지 결정
    public GameObject monologueCanvas;
    public float triggerDistance = 2f; 
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

    private bool hasTriggered = false;

    void Update()
    {
        if (hasTriggered && triggerOnce) return;

        float dist = Vector2.Distance(player.position, transform.position);
        if (dist < triggerDistance)
        {
            hasTriggered = true;
            StartCoroutine(ShowSpeechBubble());
        }
    }

    IEnumerator ShowSpeechBubble()
    {
        Transform canvasTransform = GameObject.Find("MonologueCanvas").transform;
        GameObject bubble = Instantiate(speechBubblePrefab, player.position + offset, Quaternion.identity, canvasTransform);

        RectTransform rect = bubble.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.sizeDelta = bubbleSize;
        }

        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>();
        text.text = "";

        // 한 프레임 기다리기: Layout이 제대로 계산되도록
        yield return null;

        // 생성 직후 올바른 위치로 보정 (이걸 먼저 해야 함!)
        bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);

        // 한 글자씩 타이핑 출력
        foreach (char c in monologueText)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);

            // 출력 중에도 위치 따라가게 보정
            bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
        }

        // 출력 완료 후 지속시간 동안 유지
        float timer = 0f;
        while (timer < displayDuration)
        {
            timer += Time.deltaTime;
            bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
            yield return null;
        }

        Destroy(bubble);
        monologueCanvas.gameObject.SetActive(false);
    }

}
