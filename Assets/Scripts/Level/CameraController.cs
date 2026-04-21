using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private float currentPositionX;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        currentPositionX = transform.position.x;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z),
            ref velocity, speed);
    }

    public void MoveToRoom(Transform _newRoom)
    {
        currentPositionX = _newRoom.position.x;
    }
}