using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypingEffect_Stage2: MonoBehaviour
{

    public TextMeshProUGUI dialogueText;  // TMP Text ����
    public float typingSpeed = 0.5f;     // �� ���ڴ� ������
    public float deletingSpeed = 0.03f;
    public float delayBeforeDelete = 1.0f;
    public GameObject nextButton;
    private string[] messages = {
    }; // ����� ����

    void Start() // ���� ������Ʈ�� ó�� Ȱ��ȭ�� �� �ڵ� ȣ��
    {
        if (nextButton != null)
            nextButton.SetActive(false);  // �� ���� �� ��ư�� ���� ���·� ��Ȱ��ȭȭ

        StartCoroutine(TypeSequence()); // �� �����ڸ��� TypeSequence()�� �����
    }

    IEnumerator TypeSequence() // ��ü �޼��� ���� ����ϴ� �ڷ�ƾ �Լ�
    {
        for (int i = 0; i < messages.Length; i++) // ��� ���� ��ȸ
        {
            // Ÿ���� ȿ�� 
            yield return StartCoroutine(TypeText(messages[i]));

            // ��� ����
            yield return new WaitForSeconds(delayBeforeDelete);

            // ����� (������ ������ �ƴϸ� ����)
            if (i != messages.Length - 1)
                yield return StartCoroutine(DeleteText());
        }
        // �ι�° ������� ��� ����� ���� �� ��ư�� ���̰� ����
        if (nextButton != null)
            nextButton.SetActive(true);
    }

    IEnumerator TypeText(string sentence) // ���ڿ��� �� ���ھ� ����ϴ� �ڷ�ƾ �Լ�
    {
        dialogueText.text = ""; // ������ ����Ǿ��� �ؽ�Ʈ �ʱ�ȭ 
        foreach (char c in sentence) // �� ���ھ� ���
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator DeleteText() // �ؽ�Ʈ�� �� ���ھ� ����� �ڷ�ƾ �Լ�
    {
        while (dialogueText.text.Length > 0) // �ؽ�Ʈ�� ���� ���� ���� �ݺ�
        {
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1);
            yield return new WaitForSeconds(deletingSpeed);
        }
    }

}
