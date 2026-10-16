using UnityEngine;
using UnityEngine.InputSystem;

public class CrankValue : MonoBehaviour
{
    public float value;

    #region consistent turning parameters
    private Vector3 lastRotation;
    private float startingAngle;

    private float distance;
    #endregion


    private void Awake()
    {
        lastRotation = Vector3.zero;
        startingAngle = 0;
    }

    private void Update() {
        // how far from -180 to 180 are we rotated? (0 to 1)
        //float rotation = 
        value = (transform.rotation.eulerAngles.z) / 360f;
    }

    private void OnMouseDown() {
        Vector3 startedTurningFrom = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 baseForward = (startedTurningFrom - transform.position).normalized;

        startingAngle = Mathf.Atan2(-baseForward.x, baseForward.y) * Mathf.Rad2Deg;
    }

    private void OnMouseDrag()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 forwardDirection = (mouse_pos - transform.position).normalized;

        float angle = Mathf.Atan2(-forwardDirection.x, forwardDirection.y) * Mathf.Rad2Deg;
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(lastRotation + new Vector3(0, 0, angle - startingAngle));
    }

    private void OnMouseUp() {
        lastRotation = transform.rotation.eulerAngles;
    }
}
