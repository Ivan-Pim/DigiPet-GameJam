using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLoadScene : MonoBehaviour, IHittable
{
    public int sceneID;

    public void Awake()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnHit()
    {
        LoadFunc(sceneID);
    }

    public void LoadFunc(int id)
    {
        StartCoroutine(LoadRoutine(id));
    }

    public IEnumerator LoadRoutine(int id)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(id);
    }
}
