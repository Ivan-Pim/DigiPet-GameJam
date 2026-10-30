using UnityEngine;

public class HorseBehaviour_Sound : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float speed, baseSpeed = 10f, maxSpeed = 30f;
    public bool chasing = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            Vector2 direction = Vector2.Normalize(player.transform.position - transform.position);
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
}
