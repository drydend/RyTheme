using System;
using UnityEngine;
using Zenject;

public class LevelScore : MonoBehaviour
{
    private NotesProvider _notesProvider;
    private GameConfig _gameConfig;

    public int CurrentScore { get; private set; }

    public event Action OnScoreChanged;

    public void Initialize(NotesProvider notesProvider, GameConfig gameConfig)
    {
        _notesProvider = notesProvider;
        _gameConfig = gameConfig;
    }

    private void Start()
    {
        _notesProvider.OnNotePressed += OnNotePressed;
    }

    private void OnNotePressed(Note note)
    {
        var score = GetScoreForNote(note);
        CurrentScore += score;
        OnScoreChanged?.Invoke();
    }

    private int GetScoreForNote(Note note)
    {
        if (note.DistanceToCrossPoint < 0 + _gameConfig.NoteDistanceDeltaForPerfect
            && note.DistanceToCrossPoint > 0 - _gameConfig.NoteDistanceDeltaForPerfect)
        {
            return _gameConfig.ScoreForPerfect;
        }
        else if (note.DistanceToCrossPoint < 0 + _gameConfig.NoteDistanceDeltaForGood
            && note.DistanceToCrossPoint > 0 - _gameConfig.NoteDistanceDeltaForGood)
        {
            return _gameConfig.ScoreForGood;
        }
        else if (note.DistanceToCrossPoint < 0 + _gameConfig.NoteDistanceDeltaForOk
               && note.DistanceToCrossPoint > 0 - _gameConfig.NoteDistanceDeltaForOk)
        {
            return _gameConfig.ScoreForOk;
        }

        return 0;
    }
}