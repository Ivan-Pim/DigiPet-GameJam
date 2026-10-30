using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, IHittable
{
    public int sceneToLoad = -1;
    public int unloadedScene;

    public void Awake()
    {
        if (sceneToLoad == -1) {
            int nextID = gameObject.scene.buildIndex + 1;
            sceneToLoad = nextID;
        }

        if (unloadedScene != -1) { 
            unloadedScene = gameObject.scene.buildIndex;
        }
    }

    public void OnHit()
    {
        LoadFunc(sceneToLoad, unloadedScene);
    }

    public void LoadFunc(int index, int unloadingIndex) // set unloading as -1 to not unload
    {
        LoadingManager.Instance.LoadScene(index, unloadingIndex);
    }

    public void LoadFunc(string name, int unloadingIndex)
    {
        LoadFunc(SceneManager.GetSceneByName(name).buildIndex, unloadingIndex);
    }


}
