using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorVisual;
    [SerializeField] private TilemapCollider2D tilemapCollider;
    [SerializeField] private BoxCollider2D box2DCollider;

    // Method to open the door
    public void OpenDoor()
    {
        doorVisual.SetActive(false);
        if (tilemapCollider != null)
            tilemapCollider.enabled = false;
        if ( box2DCollider != null)
            box2DCollider.enabled = false;
    }

    // Method to close the door
    public void CloseDoor()
    {
        doorVisual.SetActive(true);
        if (tilemapCollider != null)
            tilemapCollider.enabled = true;
        if (box2DCollider != null)
            box2DCollider.enabled = true;
    }
}