using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Check if an instance of MusicManager already exists
        if (instance == null)
        {
            instance = this;
            // Make this object persist across scene loads
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}