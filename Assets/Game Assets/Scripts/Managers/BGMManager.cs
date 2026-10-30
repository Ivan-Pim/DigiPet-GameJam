using UnityEngine;

public enum Music {
    None = -1,
    Desktop,
    SoundLVL,
    CinemaLVL,
    Minigame,
    WhiteNoise
}

[RequireComponent(typeof(AudioSource))]
public class BGMManager : Singleton<BGMManager>
{

    [SerializeField] private AudioClip[] bgmList;
    private AudioSource bgmSource;

    override protected void Awake()
    {
        base.Awake();
        bgmSource = GetComponent<AudioSource>();
    }

    public void SwapBGM(Music track) {
        AudioClip newClip = bgmList[(int)track];
        if (bgmSource.clip != newClip)
        {        
            bgmSource.clip = newClip;
            bgmSource.Play();
        }

    }

    public void UpdateVolume(float _volume) {
        bgmSource.volume = _volume;
    }
    public void ToggleMute(bool isMuted) {
        bgmSource.mute = isMuted;
    }
}
