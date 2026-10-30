using UnityEngine;

public class volumeSlider : MonoBehaviour
{
    public void TryUpdateVolume(float volume)
    {
        BGMManager.Instance.UpdateVolume(volume);
    }
}
