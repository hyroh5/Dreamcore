// DeathZone.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Respawning...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


