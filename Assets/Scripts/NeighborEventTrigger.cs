using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborEventTrigger : MonoBehaviour
{
    public GameObject neighbor; // 움직일 게임오브젝트
    public Transform startPoint; // 시작점 좌표
    public Transform endPoint; // 도착점 좌표
    public float moveSpeed = 2f; // 움직이는 속도 

    private Animator neighborAnimator; // 애니메이터 불러오기 
    private bool eventTriggered = false; // 이벤트 발생 유무 

    void Start()
    {
        neighbor.SetActive(false); // 시작했을땐 활성화 안된 상태 
        neighborAnimator = neighbor.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) // 충돌 감지 함수
    {
        if (!eventTriggered && other.CompareTag("Player"))
        {
            eventTriggered = true; // 집과 플레이어가 접촉했을 때 
            StartCoroutine(MoveNeighbor()); // 이웃이 걸어나온다 
        }
    }
    
    IEnumerator MoveNeighbor() // 이웃 이동 함수
    {
        neighbor.SetActive(true); // 이웃 보이기
        neighbor.transform.position = startPoint.position; // 시작위치에서 등장

        // 걷기 애니메이션 ON
        neighborAnimator.SetBool("isWalking", true);

        // 지정 위치까지 이동
        while (Vector2.Distance(neighbor.transform.position, endPoint.position) > 0.05f)
        // 현재 위치와 도착점 사이의 거리가 0.05f(오차 허용치)보다 클 때 반복
        {
            neighbor.transform.position = Vector2.MoveTowards(
                // MoveTowards() = 목표 위치로 이동하는 함수수
                neighbor.transform.position,
                endPoint.position,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 걷기 종료 → 대화 애니메이션 ON
        neighborAnimator.SetBool("isWalking", false);

        // 대화창 띄우기 (원하는 방식으로 연결)
        // DialogueManager.Instance.StartDialogue(...);
    }
}
