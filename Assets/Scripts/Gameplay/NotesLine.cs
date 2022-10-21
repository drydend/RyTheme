using System.Collections.Generic;
using UnityEngine;

public class NotesLine : MonoBehaviour
{
    [SerializeField]
    private Cross _cross;

    [SerializeField]
    private Transform _spawningPoint;
    [SerializeField]
    private Transform _crossingPoint;
    [SerializeField]
    private Transform _noteEndPoint;
    [SerializeField]
    private GameConfig _gameConfig;

    private Queue<Note> _notesQueue = new Queue<Note>();

    public Transform SpawningPoint => _spawningPoint;
    public Transform CrossingPoint => _crossingPoint;
    public Transform NoteEndPoint => _noteEndPoint;

    public void AddNoteToQueue(Note note)
    {
        _notesQueue.Enqueue(note);
        note.OnReachedEnd += () => _notesQueue.Dequeue();
    }

    public bool CheckIfNoteIsOnCross()
    {
        if (_notesQueue.TryPeek(out Note note))
        {   
            if (note.DistanceToCrossPoint < 0 + _gameConfig.NoteDistanceDeltaForOk
                && note.DistanceToCrossPoint > 0 - _gameConfig.NoteDistanceDeltaForOk)
            {
                return true;
            }
            else if (note.DistanceToCrossPoint < 0 + _gameConfig.NoteDistanceDeltaForGood
                && note.DistanceToCrossPoint > 0 - _gameConfig.NoteDistanceDeltaForGood)
            {
                return true;
            }
            else if (note.DistanceToCrossPoint < 0 + _gameConfig.NoteDistanceDeltaForPerfect
                && note.DistanceToCrossPoint > 0 - _gameConfig.NoteDistanceDeltaForPerfect)
            {
                return true;
            }
        }

        return false;
    }

    public void PressCurrentNote()
    {
        if (_notesQueue.Count == 0)
        {
            throw new System.Exception("There is no notes on line");
        }

        var currentNote = _notesQueue.Dequeue();
        currentNote.OnNotePressed();
    }

    private void OnCrossPressed()
    {
        if (_notesQueue.Count == 0)
        {
            return;
        }

        if (CheckIfNoteIsOnCross())
        {
            PressCurrentNote();
        }
    }

    private void Awake()
    {
        _cross.OnPressed += OnCrossPressed;
    }
}
