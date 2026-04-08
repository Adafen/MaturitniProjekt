using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // This method is called when the "Play" button is clicked in the main menu
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenSettings()
    {
        Debug.Log("Settings menu opened!");
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
