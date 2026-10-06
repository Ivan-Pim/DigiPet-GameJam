using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animController;
    private SpriteRenderer sprite;

    private Vector2 correspondingSpawn;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animController = GetComponentInChildren<Animator>();
        correspondingSpawn = transform.position;
    }

    private void Update()
    {
        sprite.transform.position =new Vector2(sprite.transform.position.x, Mathf.PingPong(Time.time, 1));
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
