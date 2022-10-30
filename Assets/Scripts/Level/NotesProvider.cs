using System;

public class NotesProvider
{
    public int NotesCount { get; set; }

    public event Action<Note> OnNoteReachedEnd;
    public event Action<Note> OnNotePressed;

    public bool IsThereNotesOnScene => NotesCount > 0;

    public void AddNote(Note note)
    {
        NotesCount++;

        note.OnPressed += () => OnNotePressed?.Invoke(note);

        note.OnReachedEnd += () => OnNoteReachedEnd?.Invoke(note);
    }
}
