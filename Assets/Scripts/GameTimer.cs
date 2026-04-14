using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public float elapsedTime;
    private bool isTimerRunning = true;

    // Update is called once per frame
    void Update()
    {
        // If the timer is running, update the elapsed time and the UI
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }
    private void UpdateTimerUI()
    {
        // Convert elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    // Call this method to stop the timer when the game ends
    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
