using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, IHittable
{
    public string sceneToLoad;

    public void Awake()
    {
        if (sceneToLoad == null) {
            int nextID = gameObject.scene.buildIndex + 1;
            sceneToLoad = SceneManager.GetSceneByBuildIndex(nextID).name;
        }
        
    }

    public void OnHit()
    {
        LoadFunc(sceneToLoad);
    }

    public void LoadFunc(string name, bool unload = true)
    {
        LoadingManager.Instance.LoadScene(name);
        if (unload) LoadingManager.Instance.UnloadScene(gameObject.scene.name);
    }
}
