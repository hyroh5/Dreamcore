using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    [Header("기본 설정")]
    public float weight = 1f;
    public bool canMove = true;
    public float bounceForce = 1f;
    public AudioClip hitSound;

    [Header("자동 리셋 옵션")]
    public bool autoResetPosition = false;
    public float resetDelay = 3f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Vector3 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canMove) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * bounceForce / weight, ForceMode2D.Impulse);

            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            if (autoResetPosition)
            {
                CancelInvoke(nameof(ResetPosition));
                Invoke(nameof(ResetPosition), resetDelay);
            }
        }
    }

    private void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = originalPosition;
    }
}
