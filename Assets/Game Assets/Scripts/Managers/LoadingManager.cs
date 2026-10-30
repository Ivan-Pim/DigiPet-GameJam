using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    

    public event Action OnUnload;

    public void LoadScene(int sceneID) {
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(int sceneID) {
        OnUnload.Invoke();
        SceneManager.UnloadSceneAsync(sceneID);
    }

    public void UnloadScene(string sceneName)
    {
        OnUnload.Invoke();
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
