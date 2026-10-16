using UnityEngine;

public class WaveControls : MonoBehaviour
{
    public SineWaveVisualiser waveVisualiser;
    // To ensure everything stays in 
    public float amplitudePercent = 0.5f;
    public float frequencyPercent = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveVisualiser = GetComponent<SineWaveVisualiser>();
    }

    // Update is called once per frame
    void Update()
    {
        waveVisualiser.setAmplitude(amplitudePercent);
        waveVisualiser.setFrequency(frequencyPercent);
    }
}
