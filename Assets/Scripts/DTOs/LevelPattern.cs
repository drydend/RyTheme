using System.Collections.Generic;

public class LevelPattern
{
    public TrackData TrackData { get; }
    public Dictionary<SupportedSongType, Queue<NoteData>> Notes { get; }

    public LevelPattern(Dictionary<SupportedSongType, Queue<NoteData>> notes, TrackData trackData)
    {
        TrackData = trackData;
        Notes = notes;
    }
}
