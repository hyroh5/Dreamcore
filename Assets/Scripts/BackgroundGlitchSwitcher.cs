using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundGlitchSwitcher : MonoBehaviour
{
    public Image Image_BG; // 배경 이미지 (하나만 사용)
    public GameObject GlitchOverlay; // 글리치 효과 오버레이 오브젝트
    public GameObject GlitchHouse; // 집 깜빡일 때 덧입혀지는 이미지지

    public float switchInterval = 3f; // 글리치 세션 간 간격 (기본 3초)
    public int minFlickers = 2; // 최소 깜빡임 횟수
    public int maxFlickers = 6; // 최대 깜빡임 횟수
    public float minFlickerDuration = 0.03f; // 깜빡임 최소 지속 시간 (초)
    public float maxFlickerDuration = 0.12f; // 깜빡임 최대 지속 시간 (초)

    void Start()
    {
        // 시작할 때 글리치 루틴 실행
        StartCoroutine(SwitchRoutine());
    }

    IEnumerator SwitchRoutine()
    {
        while (true)
        {
            // 일정 시간 대기 후 글리치 시작
            yield return new WaitForSeconds(switchInterval);

            // 깜빡임 횟수를 랜덤하게 결정
            int flickerCount = Random.Range(minFlickers, maxFlickers + 1);

            // 깜빡임 루프
            for (int i = 0; i < flickerCount; i++)
            {
                // 글리치 ON
                GlitchOverlay.SetActive(true);
                GlitchHouse.SetActive(true);

                // 랜덤한 시간 동안 ON 상태 유지
                yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));

                // 글리치 OFF
                GlitchOverlay.SetActive(false);
                GlitchHouse.SetActive(false);

                // 다음 깜빡임까지 랜덤 대기
                yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
            }
        }
    }
}



