using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

