using UnityEditor;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    bool isPaused = false;
    [SerializeField] GameObject pauseScreen;

    override protected void Awake()
    {
        base.Awake();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void TogglePause()
    {
        TogglePause(!isPaused);
    }

    public void TogglePause(bool willPause) {
        if (willPause) Pause();
        else Unpause();
    }
}
