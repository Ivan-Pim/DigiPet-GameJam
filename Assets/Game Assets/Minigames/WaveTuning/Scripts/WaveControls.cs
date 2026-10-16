using UnityEngine;

public class WaveControls : MonoBehaviour
{
    public SineWaveVisualiser waveVisualiser;
    public SineWaveVisualiser targetWave;

    public CrankValue AmpCrank;
    public CrankValue FreqCrank;
    // To ensure everything stays in appropriate ranges, use Lerp between limits
    public float amplitudePercent = 0.5f;
    public float frequencyPercent = 0.5f;

    public float errorMargin = 0.03f;
    public float checkTime = 0.2f;
    private float checkTimeLeft;

    void Start()
    {
        waveVisualiser = GetComponent<SineWaveVisualiser>();
        checkTimeLeft = checkTime;
    }

    void Update()
    {
        updateValues();

        checkTimeLeft -= Time.deltaTime;
        CheckSolved();
        if (checkTimeLeft <= 0) Win();
    }

    void updateValues() {
        amplitudePercent = AmpCrank.value;
        frequencyPercent = FreqCrank.value;

        waveVisualiser.setAmplitude(amplitudePercent);
        waveVisualiser.setFrequency(frequencyPercent);
    }

    private void CheckSolved() {
        float frequencyDifference = Mathf.Abs(targetWave.frequencyPercent - frequencyPercent);
        float amplitudeDifference = Mathf.Abs(targetWave.amplitudePercent - amplitudePercent);

        if ((frequencyDifference > errorMargin) || (amplitudeDifference > errorMargin))
        {
            checkTimeLeft = checkTime;
        }
    }

    private void Win()
    {
        Debug.Log("Now you really won!");
    }
}
