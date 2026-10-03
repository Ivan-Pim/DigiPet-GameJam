using UnityEngine;

public class PlayerRestart : MonoBehaviour
{
    private Vector2 spawnPos;
    private Rigidbody2D rb;

    [SerializeField] private LayerMask _damageLayer;

    void Start()
    {
        spawnPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void setSpawn(Vector2 spawnPos)
    {
        this.spawnPos = spawnPos;
    }

    public void Respawn()
    {
        rb.simulated = false;
        transform.position = spawnPos;
        rb.simulated = true;
    }
}
