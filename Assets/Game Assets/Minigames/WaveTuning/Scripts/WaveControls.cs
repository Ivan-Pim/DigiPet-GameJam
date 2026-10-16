using UnityEngine;

public class WaveControls : MonoBehaviour
{
    public SineWaveVisualiser waveVisualiser;
    public SineWaveVisualiser targetWave;
    // To ensure everything stays in appropriate ranges, use Lerp between limits
    public float amplitudePercent = 0.5f;
    public float frequencyPercent = 0.5f;

    public float errorMargin = 0.03f;

    void Start()
    {
        waveVisualiser = GetComponent<SineWaveVisualiser>();
    }

    void Update()
    {
        waveVisualiser.setAmplitude(amplitudePercent);
        waveVisualiser.setFrequency(frequencyPercent);
        CheckSolved();
    }

    private void CheckSolved() {
        float frequencyDifference = Mathf.Abs(targetWave.frequencyPercent - frequencyPercent);
        float amplitudeDifference = Mathf.Abs(targetWave.amplitudePercent - amplitudePercent);

        if (frequencyDifference < errorMargin && amplitudeDifference < errorMargin)
        {
            Debug.Log("You win!!!");
        }
    }
}
