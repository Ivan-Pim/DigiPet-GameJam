using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private UIAnimator animator;
    private Sprite[][] animationStates;
    private int[] animationIntervals;

    private void Awake()
    {
        var anims = GetComponents<AnimationHolder>();
        System.Array.Resize(ref animationStates, anims.Length);
        System.Array.Resize(ref animationIntervals, anims.Length);
        int i = 0;
        foreach (var animation in GetComponents<AnimationHolder>()) {
            animationIntervals[i] = animation.interval;
            animationStates[i] = animation.animFrames;
            ++i;
        }
    }

    public void OnClick()
    {
        int state = Random.Range(0, animationStates.Length);
        animator.PlayAnimation(animationStates[state], animationIntervals[state]); ;
    }
}
