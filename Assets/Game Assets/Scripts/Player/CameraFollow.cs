using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region REFERENCES
    public PlayerMovement playerState;
    public Transform player;
    public ShakeBehaviour screenShake;
    #endregion

    #region Movement Parameters
    [SerializeField] private float offsetX = 5, offsetY = 2, offsetZ = -20;
    [SerializeField] private float lookAheadDist = 10f, lookAheadSpeed = 10f;
    private float horizontalDeadzoneDistance = 3f;
    [SerializeField] private float speed = 45f;
    #endregion
    void Start()
    {
        player = playerState.transform;
        screenShake = GetComponent<ShakeBehaviour>();

        transform.position = player.position + new Vector3(offsetX, offsetY, offsetZ);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position + new Vector3(offsetX, offsetY, offsetZ);
        Vector3 position = transform.position;

        #region Set X

        if (Mathf.Abs(targetPosition.x - transform.position.x) > horizontalDeadzoneDistance)
        {
            if (playerState.IsFacingRight)
            {
                offsetX = Mathf.Lerp(offsetX, lookAheadDist, lookAheadSpeed * Time.deltaTime);
            }
            else
            {
                offsetX = Mathf.Lerp(offsetX, -lookAheadDist, lookAheadSpeed * Time.deltaTime);
            }
            position.x = Mathf.Lerp(transform.position.x, targetPosition.x, speed * Time.deltaTime);
        } 

        #endregion

        #region Set Y
        position.y = Mathf.Lerp(transform.position.y, targetPosition.y, speed * Time.deltaTime);
        #endregion

        #region set Z
        position.z = offsetZ;
        #endregion

        if (screenShake.shakeMagnitude > 0f) {
            transform.position = position + screenShake.translationalShake;
            transform.rotation = Quaternion.Euler(0, 0, screenShake.rotationalShake);
        } else {
            transform.position = position;
            transform.rotation = Quaternion.identity;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(player.position, new Vector2(horizontalDeadzoneDistance, horizontalDeadzoneDistance));
    }
}
