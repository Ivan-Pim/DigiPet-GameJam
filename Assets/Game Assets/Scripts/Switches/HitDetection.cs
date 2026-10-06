using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            if (collision.GetComponent<PlayerMovement>().isInGroundPound) {
                GetComponentInParent<IHittable>().OnHit();
            }
        }
    }

}
