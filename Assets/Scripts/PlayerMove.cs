using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public float maxSpeed;
    public float jumpPower;
    bool isJump = false;

    private void Awake() // 플레이어 오브젝트가 만들어졌을 때
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() // 50번/1초 -> 지속적인 키 입력은 FixedUpdate에서
                               // 플레이어가 움직이는 물리연산이므로
    {

        // @좌우이동
        float h = Input.GetAxisRaw("Horizontal");
        // h는 사용자의 좌우 입력 방향을 나타내는 정수(-1, 0, 1) 값을 담음.
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        // Vector2.right는 (1, 0) 방향 벡터니까
        // → h를 곱하면왼쪽(-1) or 오른쪽(1) 방향으로 힘을 가함

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        //강제로 maxSpeed 또는 -maxSpeed로 제한
        //이렇게 안 하면 계속 AddForce가 누적돼서 속도가 무한히 빨라질 수 있음


        //Landing Ploatform
        if (rigid.velocity.y < 0) // 내려갈 때만 스캔
        {
            Vector2 rayOrigin = rigid.position + Vector2.down * 0.5f; // 발 근처에서 시작
            Vector2 direction = Vector2.down;
            float rayLength = 0.7f;

            Debug.DrawRay(rayOrigin, direction * rayLength, Color.green);

            RaycastHit2D rayHitPlatform = Physics2D.Raycast(rayOrigin, direction, rayLength, LayerMask.GetMask("Platform"));
            if (rayHitPlatform.collider != null)
            {
                anim.SetBool("isJumping", false);
            }

            RaycastHit2D rayHitObstacle = Physics2D.Raycast(rayOrigin, direction, rayLength, LayerMask.GetMask("Obstacle"));
            if (rayHitObstacle.collider != null)
            {
                anim.SetBool("isJumping", false);
            }
        }

         

    }

    private void Update() // 60번/1초 -> 단발적인 키 입력(ex 점프)은 Update에서
    {
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);


        // @ 부드러운 감속
        if (Input.GetButtonUp("Horizontal")) // Up은 떼는 것
            // rigid.velocity.x는 캐릭터의 현재 좌우 속도
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        // 방향키에서 손을 뗐을 때 캐릭터가 너무 갑자기 멈추지 않고,
        // 속도를 절반 정도로 줄여서 부드럽게 감속되게 만들기 위한 코드
        // 1. velocity.normalized: 속도 벡터의 방향만 남기고 길이는 1로 만든 벡터
        // 2. 0.5f: 방향은 유지하면서 속도를 0.5 정도로 줄임(감속 느낌)

        // @ 방향 뒤집기
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        // 캐릭터가 왼쪽으로 이동하면(왼쪽키가 눌리고 있으면) 방향 뒤집기

        // @ 점프 구현
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // @ 애니메이션 전환
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

    }

}