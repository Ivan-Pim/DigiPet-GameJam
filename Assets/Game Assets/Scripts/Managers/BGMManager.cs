using System.Xml;
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

        ReadSettings();
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
        PlayerPrefs.SetFloat("Music Volume", _volume);
        bgmSource.volume = _volume;
    }
    public void ToggleMute(bool isMuted) {
        int toStore = isMuted ? 1 : 0;
        PlayerPrefs.SetInt("Music Mute", toStore);
        bgmSource.mute = isMuted;
    }

    private void ReadSettings()
    {
        bgmSource.volume = PlayerPrefs.GetFloat("Music Volume", 1.00f);
        bool isMuted = (PlayerPrefs.GetInt("Music Mute", 0) == 0) ? false : true;
        bgmSource.mute = isMuted;
    }
}
