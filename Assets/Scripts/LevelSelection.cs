using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;

    // Ensure that the buttons are ordered in the inspector according to their level (e.g., Level 1 button first, Level 2 button second, etc.)
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        // Start with the main menu active and the level selection panel hidden
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);

        UpdateLevelButtons();
    }

    // This method can be called by a "Play" button in the main menu
    public void OpenLevelSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    // This method can be called by a "Back" button in the level selection panel
    public void CloseLevelSelection()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void UpdateLevelButtons()
    {
        // Get the highest unlocked level from PlayerPrefs, defaulting to 1 if not set
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);


        for (int i = 0; i < levelButtons.Length; i++)
        {
            // The level index is i + 1 because levels are typically 1-indexed for players
            if (i + 1 > unlockedLevel)
            {
                levelButtons[i].interactable = false; // Disable the button if the level is locked
            }
            else
            {
                levelButtons[i].interactable = true; // Ensure the button is interactable if the level is unlocked
            }
        }
    }

    // This method is called by the level buttons, passing the corresponding scene index
    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
