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

    private void Awake() // �÷��̾� ������Ʈ�� ��������� ��
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() // 50��/1�� -> �������� Ű �Է��� FixedUpdate����
                               // �÷��̾ �����̴� ���������̹Ƿ�
    {

        // @�¿��̵�
        float h = Input.GetAxisRaw("Horizontal");
        // h�� ������� �¿� �Է� ������ ��Ÿ���� ����(-1, 0, 1) ���� ����.
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        // Vector2.right�� (1, 0) ���� ���ʹϱ�
        // �� h�� ���ϸ����(-1) or ������(1) �������� ���� ����

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        //������ maxSpeed �Ǵ� -maxSpeed�� ����
        //�̷��� �� �ϸ� ��� AddForce�� �����ż� �ӵ��� ������ ������ �� ����


        //Landing Ploatform
        if (rigid.velocity.y < 0) // ������ ���� ��ĵ
        {
            Vector2 rayOrigin = rigid.position + Vector2.down * 0.5f; // �� ��ó���� ����
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

    private void Update() // 60��/1�� -> �ܹ����� Ű �Է�(ex ����)�� Update����
    {
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);


        // @ �ε巯�� ����
        if (Input.GetButtonUp("Horizontal")) // Up�� ���� ��
            // rigid.velocity.x�� ĳ������ ���� �¿� �ӵ�
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        // ����Ű���� ���� ���� �� ĳ���Ͱ� �ʹ� ���ڱ� ������ �ʰ�,
        // �ӵ��� ���� ������ �ٿ��� �ε巴�� ���ӵǰ� ����� ���� �ڵ�
        // 1. velocity.normalized: �ӵ� ������ ���⸸ ����� ���̴� 1�� ���� ����
        // 2. 0.5f: ������ �����ϸ鼭 �ӵ��� 0.5 ������ ����(���� ����)

        // @ ���� ������
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        // ĳ���Ͱ� �������� �̵��ϸ�(����Ű�� ������ ������) ���� ������

        // @ ���� ����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // @ �ִϸ��̼� ��ȯ
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

    }

}