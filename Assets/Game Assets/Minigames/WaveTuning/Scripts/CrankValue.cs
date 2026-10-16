using UnityEngine;
using UnityEngine.InputSystem;

public class CrankValue : MonoBehaviour
{
    public float value;

    #region consistent turning parameters
    private float lastRotation;
    private float startingAngle;
    #endregion


    private void Awake()
    {
        lastRotation = 0;
        startingAngle = 0;
    }

    private void Update() {
        float rotation = ProperModulo((180f - transform.rotation.eulerAngles.z), 360);
        value = rotation / 360f;
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

        float rotationAngle = Mathf.Atan2(-forwardDirection.x, forwardDirection.y) * Mathf.Rad2Deg;
        float setAngle = lastRotation + rotationAngle - startingAngle;

        transform.rotation = Quaternion.Euler(0, 0, setAngle);
    }

    private void OnMouseUp() {
        lastRotation = transform.rotation.eulerAngles.z;
    }

    #region HELPER FUNCTIONS
    public float ProperModulo(float a, float b)
    {
        float inconsistentModulo = a % b;
        if (a >= 0) return inconsistentModulo;
        else return inconsistentModulo + Mathf.Abs(b);
    }

    private bool RotatingThroughThreshold(float angleA, float angleB) {
        bool check1 = (90 < angleA && angleA < 180) && (180 <= angleB && angleB < 270);
        bool check2 = (90 < angleB && angleB < 180) && (180 <= angleA && angleA < 270);
        return check1 || check2;
    }
    #endregion
}
