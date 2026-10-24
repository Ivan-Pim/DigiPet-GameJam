using UnityEngine;
using UnityEngine.UIElements;

public class ShakeBehaviour : MonoBehaviour
{
    private float maxShakeDuration = 1f;
    private float angle = 5f;
    private float shakePower = 1.5f;
    public float shakeMagnitude { get; private set; }
    public Vector3 translationalShake { get; private set; }
    public float rotationalShake { get; private set; }

    private void Update()
    {
        if (shakeMagnitude > 0f) {
            shakeMagnitude -= Time.deltaTime / maxShakeDuration;
            float strength = shakeMagnitude * shakeMagnitude;
            translationalShake = Random.insideUnitCircle * strength * shakePower;
            rotationalShake = Random.Range(-1f, 1f) * angle * strength;
        }
    }

    public void TriggerShake(float duration)
    {
        shakeMagnitude = Mathf.Min(1, shakeMagnitude + duration);
    }

}
