using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect_Stage1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // TMP Text 연결
    public float typingSpeed = 0.5f;     // 한 글자당 딜레이
    public float deletingSpeed = 0.03f;
    public float delayBeforeDelete = 1.0f;
    public GameObject nextButton;
    private string[] messages = {
        "Beta version...? That's... weird.",
        "Why would there be a sign like that here...?"
    }; // 출력할 문장
    // Start is called before the first frame update
    public void StartTyping() // 표지판트리거함수에서 사용
    {
        StartCoroutine(TypeSequence());
    }
    IEnumerator TypeSequence() // 전체 메세지 순차 출력하는 코루틴 함수
    {
        for (int i = 0; i < messages.Length; i++) // 모든 문장 순회
        {
            // 타이핑 효과 
            yield return StartCoroutine(TypeText(messages[i]));

            // 잠시 멈춤
            yield return new WaitForSeconds(delayBeforeDelete);

            // 지우기 (마지막 문장이 아니면 삭제)
            if (i != messages.Length - 1)
                yield return StartCoroutine(DeleteText());
        }
    }
    IEnumerator TypeText(string sentence) // 문자열을 한 글자씩 출력하는 코루틴 함수
    {
        dialogueText.text = ""; // 이전에 저장되었던 텍스트 초기화 
        foreach (char c in sentence) // 한 글자씩 출력
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
IEnumerator DeleteText() // 텍스트를 한 글자씩 지우는 코루틴 함수
    {
        while (dialogueText.text.Length > 0) // 텍스트가 남아 있을 동안 반복
        {
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1);
            yield return new WaitForSeconds(deletingSpeed);
        }
    }
}
