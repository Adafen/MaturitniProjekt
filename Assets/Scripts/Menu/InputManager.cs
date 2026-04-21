using UnityEngine;

public static class InputManager
{
    // Default keys
    public static KeyCode MoveLeft = KeyCode.A;
    public static KeyCode MoveRight = KeyCode.D;
    public static KeyCode Jump = KeyCode.Space;
    public static KeyCode BuildMode = KeyCode.B;

    [RuntimeInitializeOnLoadMethod]
    // Method to load keys from memory when the game starts
    private static void LoadKeys()
    {
        MoveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft", "A"));
        MoveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight", "D"));
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        BuildMode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BuildMode", "B"));
    }
    // Method to save keys to memory when the game is closed or when settings are changed
    public static void SaveKeys()
    {
        PlayerPrefs.SetString("LeftKey", MoveLeft.ToString());
        PlayerPrefs.SetString("RightKey", MoveRight.ToString());
        PlayerPrefs.SetString("JumpKey", Jump.ToString());
        PlayerPrefs.SetString("BuildModeKey", BuildMode.ToString());
        PlayerPrefs.Save();
    }
}