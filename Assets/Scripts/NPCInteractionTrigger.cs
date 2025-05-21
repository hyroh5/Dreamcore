using System.Collections;
using UnityEngine;

public class NPCInteractionTrigger : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 2f;

    [Header("��ȭ ������")]
    public DialogueTurn[] dialogueTurns;  // ���� �����

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
