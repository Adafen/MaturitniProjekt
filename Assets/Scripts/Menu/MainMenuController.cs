using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject timePanel;
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider volumeSlider;
    // This method is called when the "Play" button is clicked in the main menu
    private void Start()
    {
        // Ensure only the main menu is visible when the game starts
        ShowMainMenu();
        // Load the saved volume setting
        float savedVolume = PlayerPrefs.GetFloat("SavedVolume", 0.75f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        SetMusicVolume(savedVolume);
    }

    public void OpenTime()
    {
        // Shows the time panel and hides the main menu
        mainMenuPanel.SetActive(false);
        timePanel.SetActive(true);
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

        // This will quit the application when built, but does nothing in the editor
        Application.Quit();

        // This stops play mode in the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void SetMusicVolume(float volume)
    {
        // Converts the slider value (0.0 to 1.0) to a logarithmic scale for audio volume
        mainMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SavedVolume", volume);
        PlayerPrefs.Save();
    }
}
