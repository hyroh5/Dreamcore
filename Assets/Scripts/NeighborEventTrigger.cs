using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborEventTrigger : MonoBehaviour
{
    public GameObject neighbor;
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 2f;

    private Animator neighborAnimator;
    private SpriteRenderer neighborSprite;
    private bool eventTriggered = false;

    void Start()
    {
        neighbor.SetActive(false);
        neighborAnimator = neighbor.GetComponent<Animator>();
        neighborSprite = neighbor.GetComponent<SpriteRenderer>(); // ✅ flipX 제어
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!eventTriggered && other.CompareTag("Player"))
        {
            eventTriggered = true;
            StartCoroutine(NeighborSequence());
        }
    }

    IEnumerator NeighborSequence()
    {
        // 1) 등장
        neighbor.SetActive(true);
        neighbor.transform.position = startPoint.position;
        SetFacingDirection(endPoint.position); // ✅ 방향 맞추기
        neighborAnimator.SetBool("isWalking", true);

        // 2) endPoint까지 이동
        yield return StartCoroutine(MoveTo(endPoint.position));

        neighborAnimator.SetBool("isWalking", false);

        // 3) 대화 타임 (예시용 딜레이)
        yield return new WaitForSeconds(15f); 

        // 4) 복귀 준비
        neighborAnimator.SetBool("isWalking", true);
        SetFacingDirection(startPoint.position);

        // 5) startPoint로 복귀
        yield return StartCoroutine(MoveTo(startPoint.position));

        // 6) 종료
        neighborAnimator.SetBool("isWalking", false);
        neighbor.SetActive(false);
    }

    IEnumerator MoveTo(Vector2 targetPos)
    {
        while (Vector2.Distance(neighbor.transform.position, targetPos) > 0.05f)
        {
            neighbor.transform.position = Vector2.MoveTowards(
                neighbor.transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    void SetFacingDirection(Vector2 targetPos)
    {
        if (neighborSprite == null) return;

        Vector2 dir = targetPos - (Vector2)neighbor.transform.position;
        neighborSprite.flipX = dir.x < 0;
    }
}


