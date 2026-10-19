using UnityEngine;

public class CollectCoin : MonoBehaviour, ICollectible
{
    [SerializeField] private Sprite[] iconAlts;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = SelectAlt();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            OnCollection();
        }
    }

    public void OnCollection() {
        Debug.Log("You got a coin!");
        Destroy(gameObject);
    }

    private Sprite SelectAlt()
    {
        int altID = Random.Range(0, iconAlts.Length);
        return iconAlts[altID];
    }
}
