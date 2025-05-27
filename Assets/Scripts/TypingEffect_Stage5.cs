using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypingEffect_Stage5 : MonoBehaviour, ITypingEffect
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.1f;
    public float deletingSpeed = 0.0f;
    public float delayBeforeDelete = 1.0f;
    public GameObject nextButton;
    private string[] messages = {
        "I’m...",
        "flagged for deletion..."
    };

    IEnumerator TypeSequence()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            yield return StartCoroutine(TypeText(messages[i]));
            yield return new WaitForSeconds(delayBeforeDelete);
            if (i != messages.Length - 1)
                yield return StartCoroutine(DeleteText());
        }
        if (nextButton != null)
            nextButton.SetActive(true);
    }

    IEnumerator TypeText(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator DeleteText()
    {
        while (dialogueText.text.Length > 0)
        {
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1);
            yield return new WaitForSeconds(deletingSpeed);
        }
    }

    public void OnClickNextScene()
    {
        SceneManager.LoadScene("Scene6_stage6");
    }

    public void StartTyping()
    {
        StartCoroutine(TypeSequence());
    }
}

