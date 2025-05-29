using System.Collections;
using UnityEngine;
using TMPro;

public class MonologueTrigger7 : MonoBehaviour
{
    [Header("Trigger ����")]
    public Transform player; // ���� �Ӹ����� ��ǳ���� ���� �̵��ϴ��� ����
    public GameObject monologueCanvas;
    public bool triggerOnce = true;

    [Header("��� ����")]
    [TextArea(2, 2)]
    public string monologueText = "���⿡ ���� ��� ġ��";
    public float typingSpeed = 0.05f;

    [Header("��ǳ�� ������")]
    public GameObject speechBubblePrefab;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public Vector2 bubbleSize = new Vector2(300, 100);
    public float displayDuration = 3f;

    [Header("�ʼ� ����")]
    public Transform canvasTransform; // Inspector���� ���� �Ҵ�

    public bool hasTriggered = false;

    public void TriggerMonologue(string content)
    {
        if (triggerOnce && hasTriggered)
        {
            Debug.Log("TriggerMonologue �ߺ� ���� ������");
            return;
        }

        if (canvasTransform == null)
        {
            Debug.LogError("canvasTransform�� Inspector���� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        if (monologueCanvas == null)
        {
            Debug.LogError("monologueCanvas�� ��� �ֽ��ϴ�.");
            return;
        }

        if (speechBubblePrefab == null)
        {
            Debug.LogError("speechBubblePrefab�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        monologueText = content;
        hasTriggered = true;

        // Canvas �ٽ� Ȱ��ȭ
        if (!monologueCanvas.activeSelf)
        {
            monologueCanvas.SetActive(true);
            Debug.Log("monologueCanvas�� �ٽ� Ȱ��ȭ��");
        }

        Debug.Log("�� TriggerMonologue �����: " + monologueText);
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
            Debug.LogError("TMP_Text ������Ʈ�� ã�� �� �����ϴ�.");
            yield break;
        }

        text.text = "";

        yield return null; // ���̾ƿ� �ʱ�ȭ ���

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
