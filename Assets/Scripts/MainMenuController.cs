using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    // This method is called when the "Play" button is clicked in the main menu
    private void Start()
    {
        // Ensure only the main menu is visible when the game starts
        ShowMainMenu();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenSettings()
    {
        // Shows the settings panel and hides the main menu
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void ShowMainMenu()
    {
        // Shows the main menu and hides the settings panel
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Debug.Log("Player quit the game.");

        // This will quit the application when built, but does nothing in the Unity Editor
        Application.Quit();

        // This stops play mode in the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
