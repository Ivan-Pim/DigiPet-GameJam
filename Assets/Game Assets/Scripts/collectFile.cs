using UnityEngine;

public class collectFile : MonoBehaviour
{
    [SerializeField] private GameObject textFile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCollection();
        }
    }

    public void OnCollection()
    {
        textFile.SetActive(true);
        Destroy(gameObject);
    }
}
