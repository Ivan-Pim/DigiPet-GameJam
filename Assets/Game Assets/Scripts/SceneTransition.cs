using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, IHittable
{
    public int sceneToLoad = -1;

    public void Awake()
    {
        if (sceneToLoad == -1) {
            sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }
        
    }

    public void OnHit()
    {
        LoadFunc(sceneToLoad);
    }

    public void LoadFunc(int id)
    {
        SceneManager.LoadScene(id);
    }
}
