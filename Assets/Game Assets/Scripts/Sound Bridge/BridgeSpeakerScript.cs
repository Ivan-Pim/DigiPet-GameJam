using System;
using UnityEngine;

public class BridgeSpeakerScript : MonoBehaviour, IStateDevice
{
    private bool isActive;
    [SerializeField] private bool initialState;
    [SerializeField] private GameObject soundWave;
    [SerializeField] private int length = 10;
    [SerializeField] private GameObject soundBar;

    public GameObject[] segments;

    private float offsetX = 5f, offsetY = 0f;
    private float distX = 2.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // a little spaghetto code, because SwapState puts us to the other state, I assign not the state that we want at first.
        isActive = !initialState;
        SwapState();

        Array.Resize(ref segments, length);
        CreateSoundWave(length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Off(){
        isActive = false;
        soundWave.SetActive(false);
    }

    public void On()
    {
        isActive = true;
        soundWave.SetActive(true);
    }

    public void SwapState()
    {
        if (isActive) Off();
        else On();
    }

    public void CreateSoundWave(int length) {

        for (int i = 0; i < length; ++i) {
            GameObject newBar = Instantiate(soundBar, soundWave.transform);
            newBar.transform.SetLocalPositionAndRotation(new Vector3((offsetX + distX * i), offsetY, 0), Quaternion.identity);
            segments[i] = newBar;
        }
        soundWave.GetComponent<WaveController>().SetSegments(segments);
    }
}
