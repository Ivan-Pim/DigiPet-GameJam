using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, IHittable
{
    public int sceneToLoad = -1;
    public bool additive = false;

    public void Awake()
    {
        if (sceneToLoad == -1) {
            int nextID = gameObject.scene.buildIndex + 1;
            sceneToLoad = nextID;
        }
    }

    public void OnHit()
    {
        LoadFunc(sceneToLoad, additive);
    }

    public void LoadFunc(int index, bool additive) // set unloading as -1 to not unload
    {
        LoadingManager.Instance.LoadScene(index, additive);
    }

    public void LoadFunc(string name, bool additive)
    {
        LoadFunc(SceneManager.GetSceneByName(name).buildIndex, additive);
    }


}
