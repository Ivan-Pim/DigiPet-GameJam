using UnityEngine;

public class windowBreak : MonoBehaviour, IHittable
{
    public GameObject camera;
    public GameObject turnOn;
    public GameObject turnOff;

    public void OnHit()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        foreach (var col in GetComponents<Collider2D>()) {
            Destroy(col);
        }
        camera.GetComponent<ShakeBehaviour>().TriggerShake(10f);
        turnOn.SetActive(true);
        turnOff.SetActive(false);
        BGMManager.Instance.SwapBGM(Music.WhiteNoise);
    }
}
