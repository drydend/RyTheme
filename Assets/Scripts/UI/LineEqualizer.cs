using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineEqualizer : MonoBehaviour 
{   
    private LineRenderer _lineRenderer;

    [SerializeField]
    private uint _samplesDataLength = 512;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private float _minUpdateStep;
    [SerializeField]
    private float _scaleFactor = 1f;
    [SerializeField]
    private float _distanceScale = 10f;
    [SerializeField]
    private uint _numberOfPoints;

    private float[] _clipSamplesData;
    private float _currentUpdateStep;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _clipSamplesData = new float[_samplesDataLength];
    }
    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            return;
        }   

        _currentUpdateStep += Time.deltaTime;

        if (_currentUpdateStep >= _minUpdateStep)
        {
            _currentUpdateStep = 0;
            _audioSource.clip.GetData(_clipSamplesData, _audioSource.timeSamples);

            var positions = new Vector3[_clipSamplesData.Length];

            for (int i = 0; i < _clipSamplesData.Length; i++)
            {
                positions[i] = new Vector3(_clipSamplesData[i] * _scaleFactor , i * _distanceScale, 0);
            }

            _lineRenderer.positionCount = _clipSamplesData.Lengths;
            _lineRenderer.SetPositions(positions);
        }

    }
}
