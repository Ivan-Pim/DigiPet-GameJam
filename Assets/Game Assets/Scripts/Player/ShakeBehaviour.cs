using UnityEngine;
using UnityEngine.UIElements;

public class ShakeBehaviour : MonoBehaviour
{
    private float maxShakeDuration = 1f;
    private float shakeMagnitude;
    private float angle = 10f;
    private Vector3 initialPosition;
    private Vector3 translationalShake;
    private float rotationalShake;

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }


    private void Update()
    {
        if (shakeMagnitude > 0f) {
            shakeMagnitude -= Time.deltaTime / maxShakeDuration;
            float strength = shakeMagnitude * shakeMagnitude;
            translationalShake = Random.insideUnitSphere * strength;
            rotationalShake = Random.Range(-1f, 1f) * angle * strength;

            transform.localPosition = initialPosition + translationalShake;
            transform.localRotation = Quaternion.Euler(0, 0, rotationalShake);
        }
    }

    public void TriggerShake(float duration)
    {
        initialPosition = transform.localPosition;
        shakeMagnitude = Mathf.Min(1, shakeMagnitude + duration);
    }

}
