using UnityEngine;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveLeftBtnText;
    [SerializeField] private TextMeshProUGUI moveRightBtnText;
    [SerializeField] private TextMeshProUGUI jumpBtnText;
    [SerializeField] private TextMeshProUGUI buildBtnText;

    private string actionToRebind = "";
    private bool isWaitingForKey = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateKeyText(); // Initialize the button text with the current key bindings
    }
   
    public void StartRebind(string action)
    {
        if (isWaitingForKey) return; // Prevent starting a new rebind while waiting for another
        actionToRebind = action;
        isWaitingForKey = true;
        // Update the button text to indicate waiting for input
        switch (action)
        {
            case "Left":
                moveLeftBtnText.text = "Press a key...";
                break;
            case "Right":
                moveRightBtnText.text = "Press a key...";
                break;
            case "Jump":
                jumpBtnText.text = "Press a key...";
                break;
            case "Build":
                buildBtnText.text = "Press a key...";
                break;
        }
    }
    private void OnGUI()
    {
        if (isWaitingForKey)
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown && e.keyCode != KeyCode.None)
            {
                // Check if the pressed key is Escape to cancel the rebind
                if (e.keyCode == KeyCode.Escape)
                {
                    // If Escape is pressed, cancel the rebind
                    isWaitingForKey = false;
                    actionToRebind = "";
                    UpdateKeyText(); // Reset the button text to the current key bindings
                    return;
                }

                CheckForDuplicateKeys(e.keyCode); // Ensure no duplicate key bindings
                // Update the input manager with the new key binding
                switch (actionToRebind)
                {
                    case "Left":
                        InputManager.MoveLeft = e.keyCode;
                        break;
                    case "Right":
                        InputManager.MoveRight = e.keyCode;
                        break;
                    case "Jump":
                        InputManager.Jump = e.keyCode;
                        break;
                    case "Build":
                        InputManager.BuildMode = e.keyCode;
                        break;
                }
                InputManager.SaveKeys(); // Save the new key bindings
                isWaitingForKey = false; // Stop waiting for input after a key is pressed
                actionToRebind = ""; // Clear the action to rebind
                UpdateKeyText(); // Update the button text with the new key
            }
        }
    }
    // This method checks if the new key being bound is already assigned to another action and unbinds it if necessary
    private void CheckForDuplicateKeys(KeyCode newKey)
    {
        if (InputManager.MoveLeft == newKey) InputManager.MoveLeft = KeyCode.None;
        if (InputManager.MoveRight == newKey) InputManager.MoveRight = KeyCode.None;
        if (InputManager.Jump == newKey) InputManager.Jump = KeyCode.None;
        if (InputManager.BuildMode == newKey) InputManager.BuildMode = KeyCode.None;
    }
    private void UpdateKeyText()
    {
        moveLeftBtnText.text = InputManager.MoveLeft.ToString();
        moveRightBtnText.text = InputManager.MoveRight.ToString();
        jumpBtnText.text = InputManager.Jump.ToString();
        buildBtnText.text = InputManager.BuildMode.ToString();
    }
}
