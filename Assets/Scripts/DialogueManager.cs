using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;  // TMP Text 연결
    public GameObject nextButton;

    public float typingSpeed = 0.5f;     // 한 글자당 딜레이
    public float deletingSpeed = 0.03f;
    public float delayBeforeDelete = 1.0f;



    private string[] messages;// 출력할 문장
    private int currentIndex = 0;
    private bool isTyping = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartDialogue(string[] lines)
    {
        messages = lines;
        currentIndex = 0;
        nextButton.SetActive(false);
        StartCoroutine(TypeText(messages[currentIndex]));
    }

    public void OnNextButtonPressed()
    {
        if (isTyping) return;

        currentIndex++;

        if (currentIndex >= messages.Length)
        {
            nextButton.SetActive(false);
            return;
        }

        StartCoroutine(TypeText(messages[currentIndex]));
    }

    IEnumerator TypeText(string sentence) // 문자열을 한 글자씩 출력하는 코루틴 함수
    {
        isTyping = true;
        dialogueText.text = "";
        nextButton.SetActive(false); // 타이핑 시작 시 버튼 숨김

        foreach (char c in sentence) // 한 글자씩 출력
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayBeforeDelete);
        isTyping = false;
        nextButton.SetActive(true);
    }

    
    
}
