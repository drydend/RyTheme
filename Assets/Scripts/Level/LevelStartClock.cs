using System;
using System.Collections;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class LevelStartClock : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeLevelStart = 3;

    private float _currentTime;

    public float TimeBeforeStart => _timeBeforeLevelStart;
    public float TimeLeft => _timeBeforeLevelStart - _currentTime;
    public event Action OnTick;
    public event Action OnStarted;

    public IEnumerator StartClock()
    {
        OnStarted?.Invoke();

        var timeElapsed = 0f;

        while(timeElapsed < _timeBeforeLevelStart)
        {
            timeElapsed += Time.deltaTime;
            _currentTime = timeElapsed;
            OnTick?.Invoke();
            yield return null;
        }
    }

}