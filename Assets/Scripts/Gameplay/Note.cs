using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float _timeToCrossing;
    private float _movingSpeed;
    private Transform _startPoint;
    private Transform _crossPoint;
    private Transform _endPoint;
    public float CurrentPositionInTime { get; private set; }

    public event Action OnReachedEnd;
    public event Action OnPressed;
    
    public void Initialize(Transform startPoing, Transform crossPoint, Transform endPoint, float timeToCrossing)
    {
        _startPoint = startPoing;
        _crossPoint = crossPoint;
        _endPoint = endPoint;
        _timeToCrossing = timeToCrossing;
        transform.position = _startPoint.position;
        CurrentPositionInTime = timeToCrossing;

        _movingSpeed = Vector2.Distance(_startPoint.position, _crossPoint.position) / _timeToCrossing;
    }
    
    public void AdjustCurrentPositionAndTime(float adjustingTime)
    {
        CurrentPositionInTime -= adjustingTime;
        transform.position = Vector2.MoveTowards(transform.position, _endPoint.position, _movingSpeed * adjustingTime);
    }

    public void OnNotePressed()
    {   
        OnPressed();
        Destroy(gameObject);
    }

    private void Update()
    {
        CurrentPositionInTime -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _endPoint.position, _movingSpeed * Time.deltaTime);
        
        if(transform.position == _endPoint.transform.position)
        {
            OnReachedEnd?.Invoke();
            Destroy(gameObject);
        }
    }


}
