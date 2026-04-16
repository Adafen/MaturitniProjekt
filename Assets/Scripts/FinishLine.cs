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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (timerScript != null)
        {
            float finalTime = timerScript.elapsedTime;
            finalTimeText.text = "Time: " + finalTime.ToString("F2") + " s";

            string timeKey = "BestTime_Level" + currentSceneIndex;

            if (!PlayerPrefs.HasKey(timeKey) || finalTime < PlayerPrefs.GetFloat(timeKey))
            {
                PlayerPrefs.SetFloat(timeKey, finalTime);
            }
        }

        int nextLevelIndex = currentSceneIndex + 1;

        // Only save if the player reached a new highest level
        if (nextLevelIndex > PlayerPrefs.GetInt("UnlockedLevel", 1))
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevelIndex);
        }

        // Save all changes to disk
        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume the game
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // Load the next level
    }
}
