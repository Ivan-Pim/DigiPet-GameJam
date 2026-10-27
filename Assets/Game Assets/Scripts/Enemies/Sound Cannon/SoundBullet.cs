using UnityEngine;

public class SoundBullet : MonoBehaviour
{
    private float speed = 1.0f;
    private float maxDistance = Mathf.Infinity;
    private float traveled = 0f;

    // Update is called once per frame
    void Update()
    {
        float movement = Time.deltaTime * speed;
        transform.Translate(movement, 0, 0);
        traveled += movement;
        if (traveled > maxDistance) Dissapear();
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public void setMaxDistance(float maxDistance) {
        this.maxDistance = maxDistance;
    }

    public void Dissapear()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Dissapear();
    }
}
