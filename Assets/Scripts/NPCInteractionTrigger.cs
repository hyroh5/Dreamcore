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
    //매 프레임마다 플레이어와 NPC의 거리를 계속 확인
    {
        if (!hasTriggered && Vector2.Distance(player.position, transform.position) < triggerDistance)
        {
            //일정 거리(예: 2.0f) 이하로 가까워지면 한 번만 작동
            hasTriggered = true;
            DialogueSequenceController.Instance.StartSequence(() => {
            DialogueManager.Instance.StartDialogue(dialogueLines);
                // DialogueSequenceController 스크립트의 StartSequence 함수(카메라 줌인)
                // DialogueManager 스크립트의 StartDialogue 함수
            });
        }
    }
}
