using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignTrigger : MonoBehaviour
{
    public TypingEffect_Stage1 typingEffectScript; // 타이핑 실행할 스크립트 연결
    public GameObject dialogueObject; 
    private bool hasTriggered = false; // 충돌이 여러 번 발생하지 않도록 한 번만 실행되게 제어하는 플래그

    void Start()
    {
        dialogueObject.SetActive(false); // 처음엔 말풍선 안보이게
    }
    void OnTriggerEnter2D(Collider2D other) // 캐릭터가 Trigger Colider 영역에 진입했을 때 자동 실행되는 유니티 내장 함수
    {
        if (!hasTriggered && other.CompareTag("Player")) 
        // 아직 트리거 안되었고 충돌한 대상의 태그가 player일때만 실행
        {
            hasTriggered = true;
            dialogueObject.SetActive(true); // 말풍선 보이게
            typingEffectScript.StartTyping(); // 독백 시작
        }
    }
}
