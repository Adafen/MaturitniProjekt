using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI finalTimeText;

    private bool levelCompleted = false;
    private GameTimer timerScript;

    void Start()
    {
        timerScript = Object.FindFirstObjectByType<GameTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            ShowWinScreen();
            
        }
    }

    private void ShowWinScreen()
    {
        Time.timeScale = 0f; // Pause the game

        // Show the win panel
        winPanel.SetActive(true);
        // Display the final time on the win panel
        if (timerScript != null)
        {
            float finalTime = timerScript.elapsedTime;
            finalTimeText.text = "Time: " + finalTime.ToString("F2") + " s";
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume the game
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // Load the next level
    }
}
