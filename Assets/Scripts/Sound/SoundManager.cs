using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� �Ѿ�� ����
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }

    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;  // �ݺ� ���
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
