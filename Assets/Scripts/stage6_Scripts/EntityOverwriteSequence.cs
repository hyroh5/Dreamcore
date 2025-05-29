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

    [Header("���")]
    public GameObject normalBackground;
    public GameObject damagedBackground;

    [Header("���� ������Ʈ")]
    public GameObject clonePlayer;

    public GameObject damagedimageCanvas;
    public TextMeshProUGUI damagedimageText;

    private Material damagedTextMaterial;

    void Awake()
    {
        if (damagedimageText != null)
        {
            damagedTextMaterial = new Material(damagedimageText.fontMaterial);  // ������ ����
            damagedimageText.fontMaterial = damagedTextMaterial;  // �ؽ�Ʈ�� ����
        }
    }

    public void BeginOverwriteWarning()
    {
        errorCanvas.SetActive(false);
        warningPanel.SetActive(true); // ���â ����

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() => // yes ������
        {
            warningPanel.SetActive(false); // ���â ���ְ�
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
        // 2. Cloning process initiated... ������
        for (int i = 0; i < 5; i++)
        {
            cloneTextBox.text = "[Cloning process initiated...]";
            yield return new WaitForSeconds(0.3f);
            cloneTextBox.text = "";
            yield return new WaitForSeconds(0.3f);
        }

        // 3. Target / Template ���
        cloneTextBox.text = "[Cloning process initiated...]\n";
        yield return new WaitForSeconds(0.3f);
        cloneTextBox.text += "> Target: player_34\n";
        yield return new WaitForSeconds(0.3f);
        cloneTextBox.text += "> Template: player_34 v1.0";
        yield return new WaitForSeconds(0.8f);

        // 4. ��� ��ü �� �̹��� ���� �ߴ�
        normalBackground.SetActive(false);
        damagedBackground.SetActive(true);
        stage6background flasher = FindObjectOfType<stage6background>();
        if (flasher != null) flasher.StopFlashing();

        // 5. ���� �÷��̾� ����
        clonePlayer.SetActive(true);

        // 6. ���ο� �ؽ�Ʈ ���
        transferTextBox.text = "[New instance created...]\n";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "> ID: player_35\n";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "> Appearance: identical\n";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "> Behavior set: default\n";

        yield return new WaitForSeconds(0.8f);

        // 7. Preparing transfer ������
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

        // 8. Transfering... ���� ��ü
        transferTextBox.text += "\nTransfering";
        int baseLen = transferTextBox.text.Length;

        string[] dots = { ".", "..", "...", "." };
        for (int i = 0; i < 5; i++)
        {
            transferTextBox.text = transferTextBox.text.Substring(0, baseLen) + dots[i % dots.Length];
            yield return new WaitForSeconds(0.8f);
        }

        // 9. �Ϸ� �޽���
        transferTextBox.text += "\n[Transfer complete]";
        yield return new WaitForSeconds(0.3f);
        transferTextBox.text += "\n> player_35 successfully deployed";
        transferTextBox.text += "\n> instance moved to world_002";
        clonePlayer.SetActive(false);

        yield return new WaitForSeconds(0.8f);

        // 10. ����
        
        transferTextBox.gameObject.SetActive(false);
        damagedimageCanvas.SetActive(true);

        // 10. Stage7�� �� ��ȯ
        yield return new WaitForSeconds(1f); // ��ȯ ���� �ణ ������
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene7_stage7");
    }

}
