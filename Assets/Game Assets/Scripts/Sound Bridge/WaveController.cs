using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float minHeight = 1f, maxHeight = 7.5f;
    // i dunno, seems like either you get the scale every frame, or you make them heree once, cause you can't change only 1 value I guess?
    private Vector3 baseScale = new Vector3(2, 2, 1);
    private Vector3 minScale, maxScale;

    public float tempo = 1f;
    public float stepInterval = 0.2f;
    public float quietOffset = 1f;

    private GameObject[] segments;

    [Header("Composite Sine Wave Info")]
    public Vector3 values1, values2;
    private SineWave wave1, wave2;
    private float maxima;

    void OnEnable()
    {
        wave1 = new SineWave(values1);
        wave2 = new SineWave(values2);

        // VERY CRUDE APPROXIMATION OF THE MAXIMUM OF THE FUNCTION CAUSE i DUNNO HOW TO SCALE IT DOWN TO 0-1
        maxima = wave1.getAmplitude() + wave2.getAmplitude() + quietOffset;

        minScale = baseScale;
        minScale.y = minHeight;

        maxScale = baseScale;
        maxScale.y = maxHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.timeSinceLevelLoad * tempo;
        for (int i = 0; i < segments.Length; i++) {
            // negate just to have waves come from the speaker, and not from the outside
            UpdateSegment(i, -t);
        }
    }

    #region Setters
    public void SetRange(float minHeight = 0.1f, float maxHeight = 1f) { 
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
    }

    public void SetFrequency(float tempo) {
        this.tempo = tempo;
    }

    public void SetSegments(GameObject[] _segments){
        this.segments = _segments;
    }

    #endregion

    #region Sine calculations
    private float GetSignal(SineWave wave1, SineWave wave2, float t, float yOffset) {
        return wave1.Signal(t) + wave2.Signal(t) + yOffset;
    }

    private void UpdateSegment(int i, float t)
    {
        float posInWave = t + stepInterval * i;
        float strength = Mathf.Max(GetSignal(wave1, wave2, posInWave, quietOffset) / maxima, 0);
        segments[i].transform.localScale = Vector3.Lerp(minScale, maxScale, strength);
    }
    #endregion
}

class SineWave {
    float _amplitude, _frequency, _offset;
    public SineWave(float amplitude, float frequency, float offset)
    {
        _amplitude = amplitude;
        _frequency = frequency;
        _offset = offset;
    }

    public SineWave(Vector3 values)
    {
        _amplitude = values.x;
        _frequency = values.y;
        _offset = values.z;
    }

    public float Signal(float t) {
        return _amplitude * Mathf.Sin(_frequency * t + _offset);
    }

    public float getAmplitude()
    {
        return _amplitude;
    }
}