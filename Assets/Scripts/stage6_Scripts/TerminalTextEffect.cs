using System.Collections;
using UnityEngine;
using TMPro;

public class TerminalTextEffect : MonoBehaviour
{
    public TMP_Text runText;
    public TMP_Text analyzingText;
    public TMP_Text initializingText;

    public GameObject errorWorldTextPrefab;   // ERROR �ؽ�Ʈ ������ ������ ����
    public Transform playerTransform;

    public GameObject world2Component;
    public GameObject world2image;

    public GameObject damagedimageText;



    [Header("����")]
    public float typingSpeed = 0.05f;

    void Start()
    {
        damagedimageText.SetActive(false);
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        // 1. runText Ÿ�� ȿ��
        string runCommand = "> run world_diagnostic -target:world_001";
        runText.text = "";
        foreach (char c in runCommand)
        {
            runText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3f);

        // 2-1. analyzingText ù ��
        string line1 = "> analyzing structural integrity";
        analyzingText.text = line1;
        yield return StartCoroutine(AnimateEllipsis(analyzingText, 5f));

        // 2-2. ������ �м� �� ���
        analyzingText.text += "\n> memory_fragmentation = 72.4%";
        yield return new WaitForSeconds(0.5f);

        analyzingText.text += "\n> corrupted_events = 9";
        yield return new WaitForSeconds(0.5f);

        analyzingText.text += "\n> self_awareness_flag = TRUE";

        GameObject errorText = Instantiate(errorWorldTextPrefab, playerTransform);
        errorText.transform.localPosition = new Vector3(0, 0.5f, 0);

        SpriteRenderer sr = playerTransform.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.black;  // ���� ���������� ����
        }
        yield return new WaitForSeconds(1f);

        EntityOverwriteSequence eos = FindObjectOfType<EntityOverwriteSequence>();
        if (eos != null)
        {
            eos.BeginOverwriteWarning();
        }


        // 3-1. initializingText �����̱�
        string initMsg = "[initializing new instance...]";
        for (int i = 0; i < 10; i++) // �� 5��
        {
            initializingText.text = (i % 2 == 0) ? initMsg : "";
            yield return new WaitForSeconds(0.5f);
        }

        // 3-2. ���� �Ϸ� ���
        initializingText.text = "> world_002 created";
        yield return new WaitForSeconds(0.5f);

        initializingText.text += "\n> environment_seed: cloned from world_001";
        world2Component.SetActive(true);
        

        stage6background flasher = world2image.GetComponent<stage6background>();
        if (flasher != null)
        {
            damagedimageText.SetActive(true);
            flasher.StartFlashing();
        }
    }

    IEnumerator AnimateEllipsis(TMP_Text target, float duration)
    {
        float timer = 0f;
        string baseText = target.text;
        string[] dots = { ".", "..", "...", "." };

        int index = 0;
        while (timer < duration)
        {
            target.text = baseText + dots[index];
            index = (index + 1) % dots.Length;
            timer += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    

}
