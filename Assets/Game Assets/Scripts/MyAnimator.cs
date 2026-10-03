using System.Collections;
using UnityEngine;

public class MyAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    private SpriteRenderer renderer;
    [SerializeField] private int frameWait;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        PlayAnimation(frameWait, true);
    }

    public void PlayAnimation(int waitInterval, bool oneTime) {
        StartCoroutine(AnimationCoroutine(waitInterval, oneTime));
    }

    public IEnumerator AnimationCoroutine(int waitInterval, bool oneTime) {
        for (int i = 0; i < waitInterval * frames.Length; ++i) {
            if (i % waitInterval == 0) renderer.sprite = frames[i / waitInterval];
            yield return null;
        }
        if (oneTime) {
            Destroy(gameObject);
        }
    }
}
