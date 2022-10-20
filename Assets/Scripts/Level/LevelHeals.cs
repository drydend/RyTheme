using System;
using UnityEngine;
using Zenject;

public class LevelHeals : MonoBehaviour
{
    [SerializeField]
    private float _maxHeals;
    private float _missDamage;

    private float _currentHeals;
    private NotesProvider _notesProvider;
    private GameConfig _gameConfig;

    public event Action OnHealsChanged;
    public event Action OnCurrentHealsIsZero;

    public float CurrentHeals => _currentHeals;
    public float MaxHeals => _maxHeals;

    public void Initialize(NotesProvider notesProvider, GameConfig gameConfig)
    {
        _notesProvider = notesProvider;
        _gameConfig = gameConfig;
        _missDamage = gameConfig.DamageForMiss;
    }

    public void SetGameConfig(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    public void DecreaseCurrentHeals(float value)
    {
        if(value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        _currentHeals -= value;

        if(_currentHeals <= 0)
        {
            _currentHeals = 0;
            OnCurrentHealsIsZero?.Invoke();
        }

        OnHealsChanged?.Invoke();
    }

    private void Start()
    {
        _currentHeals = _maxHeals;
        OnHealsChanged?.Invoke();
        _notesProvider.OnNoteReachedEnd += (note) => DecreaseCurrentHeals(_missDamage);
    }
}