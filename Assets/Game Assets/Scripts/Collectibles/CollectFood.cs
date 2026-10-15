using UnityEngine;

public class CollectFood : MonoBehaviour, ICollectible
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            OnCollection();
        }
    }

    public void OnCollection()
    {
        Debug.Log("You got food!");
        Destroy(gameObject);
    }
}