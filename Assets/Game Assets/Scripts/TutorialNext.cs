using UnityEngine;

public class TutorialNext : MonoBehaviour
{
    [SerializeField] private GameObject nextWindow;
    private void OnDestroy()
    {
        nextWindow.SetActive(true);
    }
}
