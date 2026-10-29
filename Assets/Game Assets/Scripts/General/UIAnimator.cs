using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] baseFrames;
    private Image image;

    [SerializeField] private int frameWait;
    [SerializeField] private bool hasIdle = true;

    Coroutine current = null;

    private void Awake()
    {
        image = GetComponent<Image>();
        //PlayAnimation(frames, frameWait, true);
    }

    private void LateUpdate() {
        if (current == null && hasIdle) {
            PlayAnimation(baseFrames, frameWait, false);
        }
    }

    public void PlayAnimation(Sprite[] frames, int waitInterval, bool oneTime = false)
    {
        if (current != null) StopPrevious();
        current = StartCoroutine(AnimationCoroutine(frames, waitInterval, oneTime));
    }

    public void PlayAnimation(Sprite[] frames)
    {
        PlayAnimation(frames, frameWait, false);
    }

    private void StopPrevious() {
        StopCoroutine(current);
        current = null;
    }

    public IEnumerator AnimationCoroutine(Sprite[] frames, int waitInterval, bool oneTime)
    {
        for (int i = 0; i < waitInterval * frames.Length; ++i)
        {
            if (i % waitInterval == 0) { 
                image.sprite = frames[i / waitInterval];
            }
            yield return null;
        }
        current = null;
        if (oneTime)
        {
            Destroy(gameObject);
        }
    }
}
