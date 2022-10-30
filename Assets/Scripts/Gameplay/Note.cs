using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float _timeToCrossing;
    private float _movingSpeed;
    private Transform _startPoint;
    private Transform _crossPoint;
    private Transform _endPoint;
    private Vector3 _targetPosition;
    private float _distanceToCrossPoint;

    private bool _isPressed;
    private bool _hasReachedEnd;

    public float DistanceToCrossPoint => _distanceToCrossPoint;

    public event Action OnReachedEnd;
    public event Action OnPressed;
    
    public void Initialize(Transform startPoing, Transform crossPoint, Transform endPoint, float timeToCrossing)
    {
        _startPoint = startPoing;
        _crossPoint = crossPoint;
        _endPoint = endPoint;
        _timeToCrossing = timeToCrossing;
        transform.position = _startPoint.position;
        _targetPosition = _crossPoint.position;

        _movingSpeed = Vector2.Distance(_startPoint.position, _crossPoint.position) / _timeToCrossing;
    }
    
    public void AdjustCurrentPositionAndTime(float adjustingTime)
    {
        transform.position = Vector2.MoveTowards(transform.position, _crossPoint.position, _movingSpeed * adjustingTime);
    }

    public void OnNotePressed()
    {
        if (_hasReachedEnd)
        {
            return;
        }

        _isPressed = true;
        OnPressed?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _endPoint.position, _movingSpeed * Time.deltaTime);
        _distanceToCrossPoint = Vector2.Distance(transform.position, _crossPoint.position);

        if (transform.position == _endPoint.transform.position)
        {   
            if (!_isPressed)
            {
                _hasReachedEnd = true;
                OnReachedEnd?.Invoke();
                Destroy(gameObject);
            }
        }
    }


}
