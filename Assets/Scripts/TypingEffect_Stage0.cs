using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // TMP Text 연결
    public float typingSpeed = 1f;     // 한 글자당 딜레이
    public string fullText = "Wake up.... again"; // 출력할 문장

    void Start() // 게임 오브젝트가 처음 활성화될 때 자동 호출
    {
        StartCoroutine(TypeText()); // 씬 열리자마자 TypeText()가 실행됨
    }

    IEnumerator TypeText()
    {
        dialogueText.text = ""; // TMP 텍스트 초기화 
        foreach (char c in fullText) // 출력할 전체 문장을 한 글자씩 반복해서 출력 
        {
            dialogueText.text += c; // 기존에 출력된 텍스트 위해 한 글자씩 추가 
            yield return new WaitForSeconds(typingSpeed); // 타닥타닥의 핵심
        }
    }
}
