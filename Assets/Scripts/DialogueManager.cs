using System.Collections;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject dialogueCanvas;

    public GameObject playerTextboxObject;
    public GameObject npcTextboxObject;
    public TextMeshProUGUI playerTextbox;
    public TextMeshProUGUI npcTextbox;
    public GameObject nextButton;

    public float typingSpeed = 0.05f;
    public float delayBeforeNext = 0.5f;

    private DialogueTurn[] dialogueTurns;
    private int turnIndex = 0;
    private int lineIndex = 0;
    private bool isTyping = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartDialogue(DialogueTurn[] turns)
    {
        dialogueTurns = turns;
        turnIndex = 0;
        lineIndex = 0;
        nextButton.SetActive(false);
        StartCoroutine(TypeCurrentLine());
    }

    public void OnNextButtonPressed()
    {
        if (isTyping) return;

        lineIndex++;

        if (lineIndex >= dialogueTurns[turnIndex].lines.Length)
        {
            // 다음 턴으로
            turnIndex++;
            lineIndex = 0;

            if (turnIndex >= dialogueTurns.Length)
            {
                EndDialogue();
                return;
            }
        }

        nextButton.SetActive(false);
        StartCoroutine(TypeCurrentLine());
    }
    private void EndDialogue()
    {
        playerTextbox.text = "";
        npcTextbox.text = "";
        dialogueCanvas.SetActive(false);

        DialogueSequenceController.Instance.EndSequence();
    }

    IEnumerator TypeCurrentLine()
    {
        isTyping = true;
        nextButton.SetActive(false);

        DialogueTurn currentTurn = dialogueTurns[turnIndex];
        string sentence = currentTurn.lines[lineIndex];

        // 말풍선 초기화
        playerTextbox.text = "";
        npcTextbox.text = "";

        // 말풍선 표시 제어
        playerTextboxObject.SetActive(currentTurn.speaker == Speaker.Player);
        npcTextboxObject.SetActive(currentTurn.speaker == Speaker.NPC);

        TextMeshProUGUI targetBox = currentTurn.speaker == Speaker.Player ? playerTextbox : npcTextbox;

        foreach (char c in sentence)
        {
            targetBox.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (lineIndex == dialogueTurns[turnIndex].lines.Length - 1)
        {
            yield return new WaitForSeconds(delayBeforeNext);
            nextButton.SetActive(true);
        }
        else
        {
            lineIndex++;
            yield return new WaitForSeconds(delayBeforeNext);
            StartCoroutine(TypeCurrentLine());
        }
    }



}
