using UnityEngine;
using TMPro;
public class Stats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] levelTimeTexts;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject timePanel;

    private int startingLevelIndex = 1;

    private void Start()
    {
        UpdateTimeDisplay();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        timePanel.SetActive(false);
    }
    private void UpdateTimeDisplay()
    {
        float totalTime = 0f;
        bool hasPlayedAnyLevel = false;

        // Loop through the text array and check PlayerPrefs for each level
        for (int i = 0; i < levelTimeTexts.Length; i++)
        {
            // Calculate the actual scene index for the level
            int levelIndex = startingLevelIndex + i;
            string timeKey = "BestTime_Level" + levelIndex;

            if (PlayerPrefs.HasKey(timeKey))
            {
                // Retrieve the saved time, add to total, and update UI
                float bestTime = PlayerPrefs.GetFloat(timeKey);
                totalTime += bestTime;
                levelTimeTexts[i].text = "Level " + (i + 1) + " Best: " + bestTime.ToString("F2") + " s";
                hasPlayedAnyLevel = true;
            }
            else
            {
                // If the level hasn't been completed yet
                levelTimeTexts[i].text = "Level " + (i + 1) + " Best: -";
            }
        }

        // Update the total time text if the player has completed at least one level
        if (totalTimeText != null)
        {
            if (hasPlayedAnyLevel)
            {
                totalTimeText.text = "Total Time: " + totalTime.ToString("F2") + " s";
            }
            else
            {
                totalTimeText.text = "Total Time: 0.00 s";
            }
        }
    }
}
