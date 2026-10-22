using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    #region REFERENCES
    public PlayerMovement playerState;
    public Transform player;
    public ShakeBehaviour screenShake;
    #endregion

    #region Movement Parameters
    [SerializeField] private float offsetX = 10, offsetY = 2, offsetZ = -20;
    [SerializeField] private float lookAheadDist = 10f, lookAheadSpeed = 2f;
    private float targetOffsetX = 5;
    private float horizontalDeadzoneDistance = 4f, topDeadzoneDistance = 8f, bottomDeadzoneDistance = -1f;
    [SerializeField] private float speedX = 20f, speedY = 10;
    #endregion

    private Vector3 followPosition;
    private Vector3 targetPosition;

    void Start()
    {
        player = playerState.transform;
        screenShake = GetComponent<ShakeBehaviour>();

        transform.position = player.position + new Vector3(offsetX, offsetY, offsetZ);
        followPosition = player.position;
    }

    void LateUpdate()
    {
        Vector3 position = followPosition;

        #region Set X
        RefreshLookahead(position);
        offsetX = Mathf.Lerp(offsetX, targetOffsetX, lookAheadSpeed * Time.deltaTime);

        if (OutsideXBounds()){
            position.x = Mathf.Lerp(position.x, player.position.x, speedX * Time.deltaTime);
        }

        #endregion

        #region Set Y
        if (!playerState.IsAirborne() || OutsideYBounds()) { 
            position.y = Mathf.Lerp(position.y, player.position.y, speedY * Time.deltaTime);
        }
        
        #endregion

        #region Combine follow and offsets

        targetPosition = position + new Vector3(offsetX, offsetY, offsetZ);
        followPosition = position;

        #endregion

        #region Screen Shake
        if (screenShake.shakeMagnitude > 0f) {
            transform.position = targetPosition + screenShake.translationalShake;
            transform.rotation = Quaternion.Euler(0, 0, screenShake.rotationalShake);
        } else {
            transform.position = targetPosition;
            transform.rotation = Quaternion.identity;
        } 
        #endregion
    }

    #region Check functions
    private bool OutsideYBounds() { 
        float distY = Mathf.Abs(player.position.y - transform.position.y);
        bool top = distY > topDeadzoneDistance;
        bool bottom = distY < bottomDeadzoneDistance;

        return top || bottom;
    }

    private bool OutsideXBounds() {
        float distX = Mathf.Abs(player.position.x - followPosition.x);
        return distX > horizontalDeadzoneDistance;
    }

    private void RefreshLookahead(Vector3 position){
        if ((player.position.x - position.x) >= horizontalDeadzoneDistance) targetOffsetX = lookAheadDist;
        else if ((player.position.x - position.x) <= -horizontalDeadzoneDistance) targetOffsetX = -lookAheadDist;
    }
    #endregion


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(offsetX, offsetY, offsetZ), new Vector2(horizontalDeadzoneDistance * 2, topDeadzoneDistance - bottomDeadzoneDistance));
    }
}
