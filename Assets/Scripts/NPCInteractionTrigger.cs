using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionTrigger : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 2f;
    public string[] dialogueLines;

    private bool hasTriggered = false;

    void Update()
    //�� �����Ӹ��� �÷��̾�� NPC�� �Ÿ��� ��� Ȯ��
    {
        if (!hasTriggered && Vector2.Distance(player.position, transform.position) < triggerDistance)
        {
            //���� �Ÿ�(��: 2.0f) ���Ϸ� ��������� �� ���� �۵�
            hasTriggered = true;
            DialogueSequenceController.Instance.StartSequence(() => {
            DialogueManager.Instance.StartDialogue(dialogueLines);
                // DialogueSequenceController ��ũ��Ʈ�� StartSequence �Լ�(ī�޶� ����)
                // DialogueManager ��ũ��Ʈ�� StartDialogue �Լ�
            });
        }
    }
}
