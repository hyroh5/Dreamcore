using UnityEngine;

public class SignTrigger : MonoBehaviour
{
    public MonoBehaviour typingEffectScript; // ITypingEffect 구현체 (MonoBehaviour로 Inspector에 노출)
    public GameObject dialogueObject;
    private ITypingEffect _typingEffect;
    private bool hasTriggered = false;

    void Start()
    {
        dialogueObject.SetActive(false);
        _typingEffect = typingEffectScript as ITypingEffect;

        if (_typingEffect == null)
        {
            Debug.LogError("typingEffectScript가 ITypingEffect를 구현하지 않았습니다!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialogueObject.SetActive(true);
            _typingEffect?.StartTyping();
        }
    }
}
