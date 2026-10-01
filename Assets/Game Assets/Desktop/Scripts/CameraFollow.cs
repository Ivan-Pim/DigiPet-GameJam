using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region REFERENCES
    public Transform player;
    #endregion

    #region Movement Parameters
    public float offsetX, offsetY, offsetZ;
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offsetX, player.position.y + offsetY, offsetZ);
    }
}
