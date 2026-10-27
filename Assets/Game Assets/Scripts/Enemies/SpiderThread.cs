using UnityEngine;

public class SpiderThread : MonoBehaviour
{
    [SerializeField] private Transform spiderAnchor;
    [SerializeField] private Vector3 webAnchor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float relativeToWeb = spiderAnchor.parent.parent.InverseTransformPoint(spiderAnchor.position).y;
        StretchVertically(this.gameObject, webAnchor.y, relativeToWeb);
    }

    public void StretchVertically(GameObject _sprite, float top, float bottom) {
        Vector3 newPos = _sprite.transform.localPosition;
        newPos.y = (top + bottom) / 2f;
        Vector3 scale = _sprite.transform.localScale;
        scale.y = Mathf.Abs(top - bottom);
        _sprite.transform.localPosition = newPos;
        _sprite.transform.localScale = scale;
    }
}
