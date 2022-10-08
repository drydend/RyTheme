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
    }

    public bool CheckIfNoteIsOnCross()
    {
        if (_notesQueue.TryPeek(out Note note))
        {
            if (note.CurrentPositionInTime < 0 + _gameConfig.NoteTimeDeltaForOk
                && note.CurrentPositionInTime > 0 - _gameConfig.NoteTimeDeltaForOk)
            {
                return true;
            }
            else if (note.CurrentPositionInTime < 0 + _gameConfig.NoteTimeDeltaForGood
                && note.CurrentPositionInTime > 0 - _gameConfig.NoteTimeDeltaForGood)
            {
                return true;
            }
            else if (note.CurrentPositionInTime < 0 + _gameConfig.NoteTimeDeltaForPerfect
                && note.CurrentPositionInTime > 0 - _gameConfig.NoteTimeDeltaForPerfect)
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
        currentNote.OnPressed();
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
        else
        {
            Debug.Log("Miss");
        }
    }

    private void Awake()
    {
        _cross.OnPressed += OnCrossPressed;
    }

    private void Update()
    {
        if (_notesQueue.TryPeek(out Note note))
        {
            if (note.CurrentPositionInTime < 0 - _gameConfig.NoteTimeDeltaForOk)
            {
                _notesQueue.Dequeue();                
            }
        }
    }
}
