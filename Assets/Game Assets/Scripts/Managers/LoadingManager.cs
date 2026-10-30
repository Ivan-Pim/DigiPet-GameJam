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

    public void LoadScene(int sceneID, bool additive) {
        if (additive)
        {
            SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
            LevelManager.Instance.InitializeLevel(sceneID);
        } else
        {
            SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
            LevelManager.Instance.InitializeLevel(sceneID);
        }

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
