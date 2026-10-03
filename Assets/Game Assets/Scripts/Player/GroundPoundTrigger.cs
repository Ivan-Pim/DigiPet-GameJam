using UnityEngine;

public class GroundPoundTrigger : MonoBehaviour
{

    private PlayerMovement playerInfo;

    void Start()
    {
        playerInfo = GetComponentInParent<PlayerMovement>();
    }

    public void swapState(bool isActive)
    {
        GetComponent<Collider2D>().enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You hit something with your ground pound!");
    }
}
