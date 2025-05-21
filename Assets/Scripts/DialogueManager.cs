using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;  // TMP Text ����
    public GameObject nextButton;

    public float typingSpeed = 0.5f;     // �� ���ڴ� ������
    public float deletingSpeed = 0.03f;
    public float delayBeforeDelete = 1.0f;



    private string[] messages;// ����� ����
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

    IEnumerator TypeText(string sentence) // ���ڿ��� �� ���ھ� ����ϴ� �ڷ�ƾ �Լ�
    {
        isTyping = true;
        dialogueText.text = "";
        nextButton.SetActive(false); // Ÿ���� ���� �� ��ư ����

        foreach (char c in sentence) // �� ���ھ� ���
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayBeforeDelete);
        isTyping = false;
        nextButton.SetActive(true);
    }

    
    
}
