using System;
using System.Collections.Generic;

public class NotesProvider
{
    public List<Note> _notesOnScene = new List<Note>();

    public event Action<Note> OnNoteReachedEnd;
    public event Action<Note> OnNotePressed;

    public bool IsThereNotesOnScene => _notesOnScene.Count > 0;

    public void AddNote(Note note) 
    {
        _notesOnScene.Add(note);

        note.OnPressed += () => OnNoteReachedEnd?.Invoke(note);
        note.OnPressed += () => _notesOnScene.Remove(note);

        note.OnReachedEnd += () => OnNoteReachedEnd?.Invoke(note);
        note.OnReachedEnd += () => _notesOnScene.Remove(note);
    }
}
