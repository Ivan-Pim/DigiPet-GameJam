using System.Collections;
using UnityEngine;

public class SwitchFile : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject connectedReference;
    private IStateDevice connectedElement;

    private float bounceSpeed = 10f;
    private float comebackSpeed = 5f;
    private float bounceDistance = 1f;

    public void Awake()
    {
        connectedElement = connectedReference.GetComponent<IStateDevice>();
    }

    public void OnHit() {
        StartCoroutine(bounceRoutine());
        if (connectedElement != null) connectedElement.SwapState();
    }

    public IEnumerator bounceRoutine()
    {
        float bounced = 0;
        while (bounced < bounceDistance)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - bounceSpeed * Time.deltaTime);
            bounced += bounceSpeed * Time.deltaTime;
            yield return null;
        }
        while (bounced > 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + comebackSpeed * Time.deltaTime);
            bounced -= comebackSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
