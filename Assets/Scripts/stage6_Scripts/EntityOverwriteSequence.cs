using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EntityOverwriteSequence : MonoBehaviour
{
    [Header("UI")]
    public GameObject errorCanvas;
    public GameObject warningPanel;
    public Button yesButton;
    public TMP_Text cloneTextBox;
    public TMP_Text transferTextBox;

    [Header("배경")]
    public GameObject normalBackground;
    public GameObject damagedBackground;

    [Header("복제 오브젝트")]
    public GameObject clonePlayer;

    public GameObject damagedimageCanvas;
    public TextMeshProUGUI damagedimageText;

    private Material damagedTextMaterial;

    void Awake()
    {
        if (damagedimageText != null)
        {
            damagedTextMaterial = new Material(damagedimageText.fontMaterial);  // 복제본 생성
            damagedimageText.fontMaterial = damagedTextMaterial;  // 텍스트에 적용
        }
    }

    public void BeginOverwriteWarning()
    {
        errorCanvas.SetActive(false);
        warningPanel.SetActive(true); // 경고창 띄우기

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() => // yes 누르면
        {
            warningPanel.SetActive(false); // 경고창 없애고
            StartCoroutine(CloningSequence());
        });
    }

    public void StartOverwriteSequence()
    {
        warningPanel.SetActive(false);
        StartCoroutine(CloningSequence());
    }

    IEnumerator CloningSequence()
    {
        // 2. Cloning process initiated... 깜빡임
        for (int i = 0; i < 5; i++)
        {
            cloneTextBox.text = "[Cloning process initiated...]";
            yield return new WaitForSeconds(0.3f);
            cloneTextBox.text = "";
            yield return new WaitForSeconds(0.3f);
        }

        // 3. Target / Template 출력
        cloneTextBox.text = "[Cloning process initiated...]\n";
        yield return new WaitForSeconds(0.3f);
        cloneTextBox.text += "> Target: player_34\n";
        yield return new WaitForSeconds(0.3f);
        cloneTextBox.text += "> Template: player_34 v1.0";
        yield return new WaitForSeconds(0.8f);

        // 4. 배경 교체 및 이미지 교차 중단
        normalBackground.SetActive(false);
        damagedBackground.SetActive(true);
        stage6background flasher = FindObjectOfType<stage6background>();
        if (flasher != null) flasher.StopFlashing();

        // 5. 복제 플레이어 생성
        clonePlayer.SetActive(true);

        // 6. 새로운 텍스트 출력
        transferTextBox.text = "[New instance created...]\n";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "> ID: player_35\n";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "> Appearance: identical\n";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "> Behavior set: default\n";

        yield return new WaitForSeconds(0.8f);

        // 7. Preparing transfer 깜빡임
        for (int i = 0; i < 3; i++)
        {
            transferTextBox.text += "\n[Preparing transfer]";
            yield return new WaitForSeconds(0.3f);
            transferTextBox.text = transferTextBox.text.Replace("\n[Preparing transfer]", "");
            yield return new WaitForSeconds(0.3f);
        }


        transferTextBox.text += "\n> Assigning player_35 to world_002";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "\n> Linking environment: cloned from world_001";
        yield return new WaitForSeconds(0.8f);

        // 8. Transfering... 점만 교체
        transferTextBox.text += "\nTransfering";
        int baseLen = transferTextBox.text.Length;

        string[] dots = { ".", "..", "...", "." };
        for (int i = 0; i < 5; i++)
        {
            transferTextBox.text = transferTextBox.text.Substring(0, baseLen) + dots[i % dots.Length];
            yield return new WaitForSeconds(0.8f);
        }

        // 9. 완료 메시지
        transferTextBox.text += "\n[Transfer complete]";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "\n> player_35 successfully deployed";
        transferTextBox.text += "\n> instance moved to world_002";
        clonePlayer.SetActive(false);

        yield return new WaitForSeconds(0.8f);

        // 10. 정리
        
        transferTextBox.gameObject.SetActive(false);
        damagedimageCanvas.SetActive(true);

        // 10. Stage7로 씬 전환
        yield return new WaitForSeconds(1f); // 전환 전에 약간 딜레이
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene7_stage7");
    }

}
