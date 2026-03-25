using TMPro.Examples;
using Unity.Mathematics;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;


    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player is exiting the door
        if (collision.CompareTag("Player"))
        {
            // Determine which room the player is now in based on their position relative to the door
            if (collision.transform.position.x > transform.position.x)
            {
                // Player is moving to the next room
                cam.MoveToRoom(nextRoom);
            }
            else
            {
                // Player is moving back to the previous room
                cam.MoveToRoom(previousRoom);
            }
        }
    }

}
