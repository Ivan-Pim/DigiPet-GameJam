using UnityEngine;

public class TextFileClosing : MonoBehaviour
{
    [SerializeField] private GameObject window;

    public void CloseWindow() {
        Destroy(window);
    }
}
