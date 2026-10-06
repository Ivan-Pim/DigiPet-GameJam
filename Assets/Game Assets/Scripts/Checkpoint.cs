using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animController;

    private Vector2 correspondingSpawn;

    private void Start()
    {
        animController = GetComponentInChildren<Animator>();
        correspondingSpawn = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animController.SetTrigger("Saving");
            collision.GetComponent<PlayerRestart>().setSpawn(correspondingSpawn);
        }
    }

}
