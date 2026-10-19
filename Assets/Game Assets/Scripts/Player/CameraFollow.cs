using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region REFERENCES
    public Transform player;
    public ShakeBehaviour screenShake;
    #endregion

    #region Movement Parameters
    [SerializeField] private float offsetX = 5, offsetY = 2, offsetZ = -20;
    [SerializeField] private float speed = 45f;
    #endregion
    void Start()
    {
        screenShake = GetComponent<ShakeBehaviour>();

        transform.position = player.position + new Vector3(offsetX, offsetY, offsetZ);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position + new Vector3(offsetX, offsetY, offsetZ);
        Vector3 position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);


        if (screenShake.shakeMagnitude > 0f) {
            transform.position = position + screenShake.translationalShake;
            transform.rotation = Quaternion.Euler(0, 0, screenShake.rotationalShake);
        } else {
            transform.position = position;
            transform.rotation = Quaternion.identity;
        }
    }
}
