using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SensoryLogTyper : MonoBehaviour
{
    public TextMeshProUGUI extractingText;
    public TextMeshProUGUI visionText;
    public List<GameObject> eyeImages; // eye 자식 오브젝트들
    public GameObject background1; // Grid > background
    public GameObject background2; // Grid > background2
    public GameObject player;
    public GameObject floatingTextPrefab;

    public float blinkDuration = 5f;
    public float fillDuration = 10f;

    private int totalBars = 20;
    private List<int> glitchSteps;

    void Start()
    {
        GenerateRandomGlitchSteps();
        StartCoroutine(PlaySequence());
    }

    void GenerateRandomGlitchSteps()
    {
        glitchSteps = new List<int>();
        HashSet<int> chosen = new HashSet<int>();

        while (chosen.Count < 2) // 에러 2개만
        {
            int step = Random.Range(3, totalBars - 2);
            if (!chosen.Contains(step))
            {
                chosen.Add(step);
                glitchSteps.Add(step);
            }
        }

        glitchSteps.Sort(); // 순서 보장
    }

    IEnumerator PlaySequence()
    {
        float timer = 0f;
        bool isVisible = true;
        while (timer < blinkDuration)
        {
            extractingText.text = isVisible ? "[EXTRACTING SENSORY DATA...]" : "";
            isVisible = !isVisible;
            timer += 0.4f;
            yield return new WaitForSeconds(0.4f);
        }

        extractingText.text = "";

        string baseText = "> vision_module → ";
        for (int i = 0; i <= totalBars; i++)
        {
            float progress = i / (float)totalBars;

            if (glitchSteps.Contains(i))
            {
                visionText.text = baseText + "[ERROR...]";

                int errorIndex = glitchSteps.IndexOf(i);
                if (errorIndex == 0)
                {
                    if (background1 != null) background1.SetActive(true);
                    if (eyeImages.Count > 0) eyeImages[0].SetActive(true);
                    SpawnFloatingText("It hurts");
                }
                else if (errorIndex == 1)
                {
                    if (background2 != null) background2.SetActive(true);
                    if (eyeImages.Count > 1) eyeImages[1].SetActive(true);
                    SpawnFloatingText("help... it hurts");
                }

                yield return new WaitForSeconds(Random.Range(0.8f, 1.8f));
            }

            string bar = new string('█', i) + new string(' ', totalBars - i);
            int percent = Mathf.RoundToInt(progress * 100);
            visionText.text = baseText + bar + $" {percent}%";
            yield return new WaitForSeconds(Random.Range(0.25f, 0.55f));
        }

        visionText.text = "> vision_module → extracted";
    }

    void SpawnFloatingText(string message)
    {
        GameObject ft = Instantiate(floatingTextPrefab, player.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        ft.transform.SetParent(player.transform); // 따라다니도록
        TextMeshProUGUI text = ft.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null) text.text = message;
        Destroy(ft, 3f); // 3초 뒤 제거
    }
}
