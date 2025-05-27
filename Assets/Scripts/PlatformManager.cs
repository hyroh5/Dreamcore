using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

// 발판을 생성 및 관리하는 매니저 클래스
public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null; 
    // 싱글톤 인스턴스 (전역 접근 가능)

    [SerializeField] GameObject platform;
    // 생성할 발판 프리팹

    [SerializeField] float posX1, posX2, posX3, posY1, posY2, posY3;
    // 초기 발판 배치 위치 (X 3개, Y 하나)

    [SerializeField] float spawnTime = 2f;
    // 발판이 재생성되는 시간 지연 (초)

    void Start()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject); // 중복된 매니저 제거

        // 초기 발판 3개 생성
        Instantiate(platform, new Vector2(posX1, posY1), platform.transform.rotation);
        Instantiate(platform, new Vector2(posX2, posY2), platform.transform.rotation);
        Instantiate(platform, new Vector2(posX3, posY3), platform.transform.rotation);
    }

    // 외부에서 호출되는 발판 생성 코루틴
    IEnumerator spawnPlatform(Vector2 spawnPos)
    {
        yield return new WaitForSeconds(spawnTime); 
        // spawnTime만큼 대기 후

        Instantiate(platform, spawnPos, platform.transform.rotation);
        // 해당 위치에 발판 생성
    }
}

