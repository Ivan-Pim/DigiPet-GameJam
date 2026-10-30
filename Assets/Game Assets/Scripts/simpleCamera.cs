using TMPro;
using UnityEngine;

public class simpleCamera : MonoBehaviour
{
    private Vector3 pos;
    private ShakeBehaviour screenShake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.position;
        screenShake = GetComponent<ShakeBehaviour>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (screenShake.shakeMagnitude > 0f)
        {
            transform.position = pos + screenShake.translationalShake;
            transform.rotation = Quaternion.Euler(0, 0, screenShake.rotationalShake);
        }
        else
        {
            transform.position = pos;
            transform.rotation = Quaternion.identity;
        }
    }
}
