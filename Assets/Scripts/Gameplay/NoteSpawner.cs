using UnityEngine;
public class NoteSpawner 
{
    private NotesLine _line;
    private Note _notePrefab;
    private NotesProvider _notesProvider;

    private float _notesTimeToCrossing;

    public NoteSpawner(NotesLine line, NotesProvider notesProvider, Note notePrefab, float noteTimeToCrossing)
    {
        _line = line;
        _notesProvider = notesProvider; 
        _notesTimeToCrossing = noteTimeToCrossing;
        _notePrefab = notePrefab;
    }
    
    public void SpawnNote(float adjustingTime)
    {
        var note = Object.Instantiate(_notePrefab, _line.transform);
        var directionVector = _line.CrossingPoint.position - _line.SpawningPoint.position;
        var rotationAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        note.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));

        note.Initialize(_line.SpawningPoint, _line.CrossingPoint, _line.NoteEndPoint, _notesTimeToCrossing);
        note.AdjustCurrentPositionAndTime(adjustingTime);
        _line.AddNoteToQueue(note);
        _notesProvider.AddNote(note);
    }
}

