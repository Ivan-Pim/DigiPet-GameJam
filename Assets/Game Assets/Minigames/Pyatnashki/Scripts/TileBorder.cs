using System.Collections;
using UnityEngine;

public class TileBorder : MonoBehaviour
{
    
    public void Dissapear(float time){
        StartCoroutine(DisappearRoutine(time));
    }

    private IEnumerator DisappearRoutine(float time)
    {
        SpriteRenderer image = gameObject.GetComponent<SpriteRenderer>();
        image.color = new Color(1f, 1f, 1f, 0f);
        float progress = 0;
        while (progress < time)
        {
            progress += Time.deltaTime;
            image.color = new Color(1f, 1f, 1f, 1 - progress / time);
            yield return null;
        }
    }
}
