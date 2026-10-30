using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Music[] Level_tracks;

    override protected void Awake()
    {
        base.Awake();
    }

    public void InitializeLevel(int id){
        BGMManager.Instance.SwapBGM(Level_tracks[id]);
    }

    public void InitializeLevel(string name)
    {
        InitializeLevel(SceneManager.GetSceneByName(name).buildIndex);
    }
}
