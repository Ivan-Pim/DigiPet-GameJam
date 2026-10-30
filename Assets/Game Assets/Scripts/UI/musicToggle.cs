using UnityEngine;
using UnityEngine.UI;

public class musicToggle : MonoBehaviour
{
    Image toggleVisual;
    [SerializeField] Sprite[] muteToggles = new Sprite[2];

    private void Awake()
    {
        toggleVisual = GetComponent<Image>();
    }

    public void TryMute(bool wantMute)
    {
        toggleVisual.sprite = wantMute ? muteToggles[1] : muteToggles[0];
        BGMManager.Instance.ToggleMute(wantMute);
    }
}
