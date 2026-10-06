using UnityEngine;

public class SwayingFloat : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float amplitude = 1f;
    private Vector2 basePos;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        basePos = transform.position;
    }

    void Update()
    {
        sprite.transform.position = new Vector2(basePos.x, basePos.y + Mathf.PingPong(Time.time * speed, amplitude));
    }
}
