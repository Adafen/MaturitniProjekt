using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reload the current scene to reset the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
