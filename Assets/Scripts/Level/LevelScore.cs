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
        if (note.CurrentPositionInTime < 0 + _gameConfig.NoteTimeDeltaForPerfect
            && note.CurrentPositionInTime > 0 - _gameConfig.NoteTimeDeltaForPerfect)
        {
            return _gameConfig.ScoreForPerfect;
        }
        else if (note.CurrentPositionInTime < 0 + _gameConfig.NoteTimeDeltaForGood
            && note.CurrentPositionInTime > 0 - _gameConfig.NoteTimeDeltaForGood)
        {
            return _gameConfig.ScoreForGood;
        }
        else if (note.CurrentPositionInTime < 0 + _gameConfig.NoteTimeDeltaForOk
               && note.CurrentPositionInTime > 0 - _gameConfig.NoteTimeDeltaForOk)
        {
            return _gameConfig.ScoreForOk;
        }

        return 0;
    }
}