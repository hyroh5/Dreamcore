using System.Collections;
using UnityEngine;
using TMPro;

public class MonologueTrigger : MonoBehaviour
{
    [Header("Trigger ����")]
    public Transform player; // ���� �Ӹ����� ��ǳ���� ���� �̵��ϴ��� ����
    public GameObject monologueCanvas;
    public float triggerDistance = 2f; 
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

        // �� ������ ��ٸ���: Layout�� ����� ���ǵ���
        yield return null;

        // ���� ���� �ùٸ� ��ġ�� ���� (�̰� ���� �ؾ� ��!)
        bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);

        // �� ���ھ� Ÿ���� ���
        foreach (char c in monologueText)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);

            // ��� �߿��� ��ġ ���󰡰� ����
            bubble.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
        }

        // ��� �Ϸ� �� ���ӽð� ���� ����
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
