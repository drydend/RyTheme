using System.Collections.Generic;

public class ParsedTrackData
{
    public readonly string SmFileName;
    public readonly string Title;
    public readonly string Artist;
    public readonly string Genre;
    public readonly string BannerName;
    public readonly string BackgroundName;
    public readonly string Music;
    public readonly string BPM;
    public readonly float SongTimeOffset;
    public readonly Dictionary<LevelType, LevelDifficulty> LevelDifficulty;
    public readonly Dictionary<LevelType, Queue<NoteData>> LevelPatterns;
    public ParsedTrackData(string smFileName,string title, string artist, string genre, string bannerName, string backgroundName,
        string music, string BPM, float timeOffset, Dictionary<LevelType, LevelDifficulty> levelDifficulty,
        Dictionary<LevelType, Queue<NoteData>> levelPatterns)
    {   
        SmFileName = smFileName;
        Title = title;
        Artist = artist;
        Genre = genre;
        BannerName = bannerName;
        BackgroundName = backgroundName;
        Music = music;
        this.BPM = BPM;
        SongTimeOffset = timeOffset;
        LevelDifficulty = levelDifficulty;
        LevelPatterns = levelPatterns;
    }
}
