using UnityEngine;

public class BridgeSpeakerScript : MonoBehaviour, IStateDevice
{
    private bool isActive;
    [SerializeField] private bool initialState;
    [SerializeField] private GameObject soundWave;
    [SerializeField] private int length = 10;
    [SerializeField] private GameObject soundBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        float baseX = transform.position.x + 5;
        float distX = 2.2f;
        float baseY = transform.position.y;

        Vector2 forward = transform.InverseTransformDirection(Vector2.right);
        for (int i = 0; i < length; ++i) {
            GameObject newBar = Instantiate(soundBar, soundWave.transform, false);
            newBar.transform.position = new Vector2((baseX + distX * i), baseY);
        }
    }
}
