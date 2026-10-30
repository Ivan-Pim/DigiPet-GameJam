using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void TryPause(bool pause) {
        PauseManager.Instance.TogglePause(pause);
    }

    public void PauseToggle()
    {
        PauseManager.Instance.TogglePause();
    }
}
