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

    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
        Array.Resize(ref segments, length);
    }

    void Start()
    {
        // a little spaghetto code, because SwapState puts us to the other state, I assign not the state that we want at first.
        isActive = !initialState;
        SwapState();

        
        CreateSoundWave(length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Off(){
        isActive = false;
        soundWave.SetActive(false);
        animator.SetBool("isActive", isActive);
    }

    private void On()
    {
        isActive = true;
        soundWave.SetActive(true);
        animator.SetBool("isActive", isActive);
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
