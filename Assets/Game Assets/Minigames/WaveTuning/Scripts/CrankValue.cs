using UnityEngine;
using UnityEngine.InputSystem;

public class CrankValue : MonoBehaviour
{
    public float value;
    private Vector3 lastRotation;

    private void Awake()
    {
        lastRotation = Vector3.zero;
    }

    private void Update() {
        // how far from -180 to 180 are we rotated? (0 to 1)
        //float rotation = 
        value = (transform.rotation.eulerAngles.z) / 360f;
    }

    private void OnMouseDrag()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 forwardDirection = (mouse_pos - new Vector2(transform.position.x, transform.position.y)).normalized;

        float angle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(lastRotation + new Vector3(0, 0, angle - 90));
    }

    private void OnMouseUp() {
        lastRotation = transform.rotation.eulerAngles;
    }
}
