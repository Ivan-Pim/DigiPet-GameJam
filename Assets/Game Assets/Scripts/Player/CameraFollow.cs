using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region REFERENCES
    public GameObject player;
    public Transform playerTransform;
    #endregion

    #region Movement Parameters
    public float offsetX, offsetY, offsetZ;
    #endregion
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + offsetX, playerTransform.position.y + offsetY, offsetZ);
    }
}
