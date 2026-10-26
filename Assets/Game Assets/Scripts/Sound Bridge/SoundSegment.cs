using NUnit.Framework;
using UnityEngine;

public class SoundSegment : MonoBehaviour
{
    private Collider2D collider;

    public Vector2 top {  get; private set; }
    bool rising = false;

    [SerializeField] LayerMask relevantLayers;
    [SerializeField] Vector2 impactSize;
    public float impactForce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.bounds.max.y > top.y)
        {
            rising = true;
        }
        else if (rising)
        {
            rising = false;
            foreach (Collider2D hit in Physics2D.OverlapBoxAll(top, impactSize, 0, relevantLayers)) {
                Debug.Log(hit.name);
                if (hit != null && hit.CompareTag("Player"))
                {
                    hit.attachedRigidbody.AddForceY(impactForce, ForceMode2D.Impulse);
                }
            }
            
        }
        top = collider.bounds.center + new Vector3(0, collider.bounds.extents.y);
    }

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(top, impactSize);
    }
    #endregion

}
