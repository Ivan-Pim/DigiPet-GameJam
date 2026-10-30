using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    

    public event Action OnUnload;

    #region Loading overloads

    override protected void Awake()
    {
        base.Awake();
        OnUnload += Test;
    }

    public async Task LoadScene(int sceneID, int unload) {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
        LevelManager.Instance.InitializeLevel(sceneID);
        await loading;
        if (unload != -1) UnloadScene(unload); 

    }
    #endregion

    #region Unloading methods
    public void UnloadScene(int sceneID) {
        OnUnload.Invoke();
        SceneManager.UnloadSceneAsync(sceneID);
    }

    public void UnloadScene(string sceneName)
    {
        OnUnload.Invoke();
        SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion

    public void Test()
    {
        Debug.Log("Not doing nothing");
    }
}
