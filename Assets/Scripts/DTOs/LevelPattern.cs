using System.Collections.Generic;

public class LevelPattern
{
    public ParsedTrackData TrackData { get; }
    public Dictionary<LevelType, Queue<NoteData>> Notes { get; }

    public LevelPattern(Dictionary<LevelType, Queue<NoteData>> notes, ParsedTrackData trackData)
    {
        TrackData = trackData;
        Notes = notes;
    }
}
