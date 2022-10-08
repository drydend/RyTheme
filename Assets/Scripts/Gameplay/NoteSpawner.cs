﻿using UnityEngine;
public class NoteSpawner 
{
    private NotesLine _line;
    private Note _notePrefab;

    private float _notesTimeToCrossing;

    public NoteSpawner(NotesLine line, Note notePrefab,float noteTimeToCrossing)
    {
        _line = line;
        _notesTimeToCrossing = noteTimeToCrossing;
        _notePrefab = notePrefab;
    }
    
    public void SpawnNote(float adjustingTime)
    {
        var note = Object.Instantiate(_notePrefab, _line.transform);
        note.Initialize(_line.SpawningPoint, _line.CrossingPoint, _line.NoteEndPoint, _notesTimeToCrossing);
        note.AdjustCurrentPositionAndTime(adjustingTime);
        _line.AddNoteToQueue(note);
    }
}

