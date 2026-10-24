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
    [SerializeField] private float offsetX = 10, offsetY = 5, offsetZ = -20;
    [SerializeField] private float speedX = 20f, speedY = 4f;
    [SerializeField] private float targetOffsetX = 5, lookAheadDist = 10f, lookAheadSpeed = 2f;
    private float targetOffsetY = 5, lookUp = 15f, restingY = 7f, lookUpSpeed = 2f;

    private float horizontalDeadzoneDistance = 4f, topDeadzoneDistance = 13f, bottomDeadzoneDistance = -6f;
    private float dampingTimerX, dampingTime = 0.25f;
    private bool outOfYBounds = false;
    #endregion

    #region Locking Parameters
    bool LockX = false, LockY = false;
    float anchorX, anchorY;
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
        if (!LockX)
        {
            RefreshLookahead(followPosition);
            offsetX = Mathf.Lerp(offsetX, targetOffsetX, lookAheadSpeed * Time.deltaTime);

            if (OutsideXBounds() || dampingTimerX > 0){
                position.x = Mathf.Lerp(position.x, player.position.x, speedX * Time.deltaTime);
                dampingTimerX -= Time.deltaTime;
            }
            if (OutsideXBounds()) dampingTimerX = dampingTime;
        } else {
            position.x = Mathf.Lerp(position.x, anchorX, speedX * Time.deltaTime);
        }

        #endregion

        #region Set Y
        if (!LockY)
        {
            RefreshLookup(followPosition);
            offsetY = Mathf.Lerp(offsetY, targetOffsetY, lookUpSpeed * Time.deltaTime);

            OutsideYBounds();

            if (!playerState.IsAirborne() || outOfYBounds) { 
                position.y = Mathf.Lerp(position.y, player.position.y, speedY * Time.deltaTime);
            }
            if (!playerState.IsAirborne() && !OutsideYBounds()) outOfYBounds = false;
        } else {
            position.y = Mathf.Lerp(position.x, anchorX, speedX * Time.deltaTime);
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

        if (top || bottom) outOfYBounds = true;

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

    private void RefreshLookup(Vector3 position) {
        if ((player.position.y - position.y) >= topDeadzoneDistance)
            targetOffsetY = lookUp;
        else if ((player.position.y - position.y) <= bottomDeadzoneDistance)
            targetOffsetY = -lookUp;
        else
            targetOffsetY = restingY;
    }
    #endregion


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(offsetX, offsetY, offsetZ), new Vector2(horizontalDeadzoneDistance * 2, topDeadzoneDistance - bottomDeadzoneDistance));
    }
}
