using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private LayerMask activationLayers;

    // What happens when the switch is pressed down
    public UnityEvent onSwitchPressed;
    // What happens when the object leaves the switch
    public UnityEvent onSwitchReleased;

    private int objectsOnSwitch = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the entering object is in the activation layers
        if (((1 << collision.gameObject.layer) & activationLayers) != 0)
        {
            objectsOnSwitch++;

            // If this is the first object to step on it, trigger the pressed event
            if (objectsOnSwitch == 1)
            {
                onSwitchPressed.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the exiting object is in the activation layers
        if (((1 << collision.gameObject.layer) & activationLayers) != 0)
        {
            objectsOnSwitch--;

            // If there are no more objects on the switch, invoke the release event
            if (objectsOnSwitch <= 0)
            {
                objectsOnSwitch = 0;
                onSwitchReleased.Invoke();
            }
        }
    }
}