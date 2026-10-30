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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().isInGroundPound)
            {
                GetComponentInParent<IHittable>().OnHit();
            }
        }
    }

}
