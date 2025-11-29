using UnityEngine;
using UnityEngine.InputSystem;

public class toggleDoor : MonoBehaviour
{
    public enum DoorAxis { X, Y, Z }
    [SerializeField] private DoorAxis rotationAxis = DoorAxis.Y;

    [SerializeField] private Transform door;
    [SerializeField] private float openAngle = -90f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool opening = false; // This script still manages the lerp state

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.localRotation;
        Vector3 axis = rotationAxis == DoorAxis.X ? Vector3.right :
                       rotationAxis == DoorAxis.Z ? Vector3.forward : Vector3.up;
        openRotation = closedRotation * Quaternion.Euler(axis * openAngle);
    }
    
    // No change to Update
    void Update()
    {
        door.localRotation = Quaternion.Lerp(
            door.localRotation,
            opening ? openRotation : closedRotation,
            Time.deltaTime * speed
        );
    }

    /// <summary>
    /// This is the new public method that our 'door.cs' coordinator will call.
    /// </summary>
    public void SetState(bool open)
    {
        opening = open;
    }

    // --- REMOVED ToggleDoor() METHOD ---
    // public void ToggleDoor() { ... }
}