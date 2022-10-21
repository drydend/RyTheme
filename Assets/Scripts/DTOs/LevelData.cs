using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public readonly string Title;
    public readonly string Artist;
    public readonly Sprite Banner;
    public readonly Sprite BackgroundName;
    public readonly AudioClip Music;
    public readonly string BPM;
    public readonly float SongTimeOffset;
    public readonly LevelType CurrentLevelType;
    public readonly LevelDifficulty LevelDifficulty;
    public readonly Queue<NoteData> LevelsPattern;

    public LevelData(ParsedTrackData parsedTrackData, Sprite banner, Sprite backgroundName,
        AudioClip music, LevelType levelType)
    {
        Title = parsedTrackData.Title;
        Artist = parsedTrackData.Artist;
        Banner = banner;
        BackgroundName = backgroundName;
        Music = music;
        BPM = parsedTrackData.BPM;
        SongTimeOffset = parsedTrackData.SongTimeOffset;
        CurrentLevelType = levelType;
        LevelDifficulty = parsedTrackData.LevelDifficulty[levelType];
        LevelsPattern = parsedTrackData.LevelPatterns[levelType];
    }
}
