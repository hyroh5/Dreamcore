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

    public AudioClip painSound;
    private AudioSource audioSource;

    [Header("외부 참조")]
    public MonologueTrigger7 monologueTrigger; // Inspector에서 직접 할당!

    void Start()
    {
        GenerateRandomGlitchSteps();
        StartCoroutine(PlaySequence());

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void GenerateRandomGlitchSteps()
    {
        glitchSteps = new List<int>();
        HashSet<int> chosen = new HashSet<int>();

        while (chosen.Count < 2)
        {
            int step = Random.Range(3, totalBars - 2);
            if (!chosen.Contains(step))
            {
                chosen.Add(step);
                glitchSteps.Add(step);
            }
        }

        glitchSteps.Sort();
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

                    if (monologueTrigger != null)
                        monologueTrigger.TriggerMonologue("It hurts");

                }
                else if (errorIndex == 1)
                {
                    if (background2 != null) background2.SetActive(true);
                    if (eyeImages.Count > 1) eyeImages[1].SetActive(true);

                    if (monologueTrigger != null)
                        monologueTrigger.TriggerMonologue("help... it hurts");
                    if (painSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(painSound);
                    }
                }

                yield return new WaitForSeconds(Random.Range(0.8f, 1.8f));
            }

            string bar = new string('█', i) + new string(' ', totalBars - i);
            int percent = Mathf.RoundToInt(progress * 100);
            visionText.text = baseText + bar + $" {percent}%";
            yield return new WaitForSeconds(Random.Range(0.25f, 0.55f));
        }

        visionText.text = "> vision_module → extracted";


        if (monologueTrigger != null)
        {
            monologueTrigger.TriggerMonologue("I cannot see anything");
            yield return new WaitForSeconds(3f);
            monologueTrigger.hasTriggered = false;
            monologueTrigger.TriggerMonologue("it hurts");
            yield return new WaitForSeconds(3f);
        }
    }

    void SpawnFloatingText(string message)
    {
        GameObject ft = Instantiate(floatingTextPrefab, player.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        ft.transform.SetParent(player.transform);
        TextMeshProUGUI text = ft.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null) text.text = message;
        Destroy(ft, 3f);
    }
}
