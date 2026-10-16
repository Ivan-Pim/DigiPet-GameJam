using UnityEngine;

public class SineWaveVisualiser : MonoBehaviour
{
    public LineRenderer myLine;
    public int points;
    public float length = 1;
    public float moveSpeed = 5;
    public bool randomised = true;
    public float amplitude {get; private set;}
    public float frequency {get; private set;}
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
            //float y = amplitude * Mathf.Sin((frequency * x + Time.timeSinceLevelLoad * moveSpeed));
            myLine.SetPosition(i, new Vector2(x, y));
        }
    }

    private void InitialiseValues(bool randomised)
    {
        if (randomised) {
            setAmplitude(Random.Range(0f, 1f));
            setFrequency(Random.Range(0f, 1f));
        } else {
            setAmplitude(0.5f);
            setFrequency(0.5f);
        }
    }

    public void setFrequency(float inputPercent) {
        this.frequency = Mathf.Lerp(minFrequency, maxFrequency, inputPercent);
    }

    public void setAmplitude(float inputPercent) {
        this.amplitude = Mathf.Lerp(minAmplitude, maxAmplitude, inputPercent);
    }
}
