using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallingtile : MonoBehaviour
{
    [SerializeField] float fallTime = 0.5f, destroyTime = 2f;
    // Inspector에서 조절 가능한 발판 떨어지는 대기 시간과 파괴 시간
    // serializefield는 private 변수도 인스펙터에 노출시킴

    Rigidbody2D rb; // 발판에 연결된 Rigidbody2D 컴포넌트를 담을 변수

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        // 게임 오브젝트에서 Rigidbody2D 컴포넌트를 찾아 할당
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player")) 
        // 충돌한 게임 오브젝트의 이름이 "Player"일 경우에만 실행
        {
            PlatformManager.Instance.StartCoroutine("spawnPlatform",
                new Vector2(transform.position.x, transform.position.y));
            // PlatformManager 싱글톤을 통해 새로운 발판을 현재 위치에 생성 요청 (코루틴 실행)

            Invoke("FallPlatform", fallTime);
            // fallTime(0.5초) 후에 FallPlatform 메서드 호출

            Destroy(gameObject, destroyTime);
            // destroyTime(2초) 후에 이 발판 오브젝트를 삭제
        }
    }

    void FallPlatform()
    {
        rb.isKinematic = false; 
        // 발판이 물리의 영향을 받게 하여 낙하하도록 설정
    }
}
