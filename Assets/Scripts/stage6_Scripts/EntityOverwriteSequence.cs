using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        warningPanel.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() =>
        {
            warningPanel.SetActive(false);
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
        // 2. Cloning process initiated... 깜박임
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

        // 4. 배경 교체
        normalBackground.SetActive(false);
        damagedBackground.SetActive(true);

        // → 이미지 교차 중단
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
            transferTextBox.text = transferTextBox.text.Replace("[Preparing transfer]", "");
            yield return new WaitForSeconds(0.3f);
        }

        transferTextBox.text += "\n> Assigning player_35 to world_002";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "\n> Linking environment: cloned from world_001";
        yield return new WaitForSeconds(0.8f);

        // 8. Transfering... 점 움직임
        string[] dots = { ".", "..", "...", "." };
        for (int i = 0; i < 5; i++)
        {
            transferTextBox.text += $"\nTransfering{dots[i % dots.Length]}";
            yield return new WaitForSeconds(0.8f);
        }

        transferTextBox.text += "\n[Transfer complete]";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "\n> player_35 successfully deployed";
        transferTextBox.text += "\n> instance moved to world_002";

        yield return new WaitForSeconds(0.8f);
        clonePlayer.SetActive(false);

        // 9. 텍스트 숨기고 이미지 캔버스 전환
        transferTextBox.gameObject.SetActive(false);
        damagedimageCanvas.SetActive(true);

        StartCoroutine(FlickerDamagedText());
    }

    IEnumerator FlickerDamagedText()
    {
        if (damagedTextMaterial == null)
            yield break;

        for (int i = 0; i < 20; i++)
        {
            damagedTextMaterial.SetFloat("_Sharpness", -1);  // 깨짐 효과
            yield return new WaitForSeconds(0.05f);
            damagedTextMaterial.SetFloat("_Sharpness", 0);   // 정상
            yield return new WaitForSeconds(0.05f);
        }
    }
}
