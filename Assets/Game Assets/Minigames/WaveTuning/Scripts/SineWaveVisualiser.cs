using UnityEngine;

public class SineWaveVisualiser : MonoBehaviour
{
    #region Common Fields
    public LineRenderer myLine;
    public int points;
    public float length = 1;
    public float moveSpeed = 5;
    public bool randomised = true;
    #endregion

    #region Wave parameters
    private float amplitude;
    private float frequency;

    public float frequencyPercent { get; private set; }
    public float amplitudePercent { get; private set; }
    #endregion

    #region Ranges
    private float minAmplitude = 0.5f, maxAmplitude = 3f;
    private float minFrequency = 0.5f, maxFrequency = 8f;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        InitialiseValues(randomised);
    }
    // Update is called once per frame
    void Update()
    {
        DrawSineWave(length);
    }

    private void DrawSineWave(float length) {
        float xStart = -Mathf.PI * length;
        float xFinish = Mathf.PI * length;

        myLine.positionCount = points;
        for (int i = 0;i < points; ++i) {
            float progress = (float) i / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin(Mathf.Lerp(0, 2 * Mathf.PI, progress) * frequency + Time.timeSinceLevelLoad * moveSpeed);
            myLine.SetPosition(i, new Vector2(x, y));
        }
    }

    private void InitialiseValues(bool randomised)
    {
        if (randomised) {
            frequencyPercent = 0.01f * Random.Range(0, 101);
            amplitudePercent = 0.01f * Random.Range(0, 101);
        } else {
            frequencyPercent = 0.5f;
            amplitudePercent = 0.5f;
        }
        setAmplitude(amplitudePercent);
        setFrequency(frequencyPercent);
    }

    public void setFrequency(float inputPercent) {
        this.frequencyPercent = inputPercent;
        this.frequency = Mathf.Lerp(minFrequency, maxFrequency, frequencyPercent);
    }

    public void setAmplitude(float inputPercent) {
        this.amplitudePercent = inputPercent;
        this.amplitude = Mathf.Lerp(minAmplitude, maxAmplitude, amplitudePercent);
    }
}
