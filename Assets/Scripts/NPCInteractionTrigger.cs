using System.Collections;
using UnityEngine;

public class NPCInteractionTrigger : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 2f;

    [Header("대화 시퀀스")]
    public DialogueTurn[] dialogueTurns;  // 구조 변경됨

    private bool hasTriggered = false;

    void Update()
    {
        if (!hasTriggered && Vector2.Distance(player.position, transform.position) < triggerDistance)
        {
            hasTriggered = true;

            DialogueSequenceController.Instance.StartSequence(() => {
                DialogueManager.Instance.StartDialogue(dialogueTurns);
            });
        }
    }
}
