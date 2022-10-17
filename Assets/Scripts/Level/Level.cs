using System.Collections.Generic;

public class Level
{
    private AudioPlayer _audioPlayer;
    private StoryLevel _storyLevel;
    private LevelData _levelData;

    private LevelType _currentLevelType;
    private List<NoteSpawner> _spawners;

    private Queue<NoteData> _currentNotes;
    private float _noteAnimationDuration;
    public Level(AudioPlayer audioPlayer, StoryLevel track, LevelData levelData,
        List<NoteSpawner> spawners, float noteAnimationDuration)
    {
        _audioPlayer = audioPlayer;
        _storyLevel = track;
        _levelData = levelData;
        _currentNotes = levelData.LevelsPattern;
        _spawners = spawners;
        _currentLevelType = levelData.CurrentLevelType;
        _noteAnimationDuration = noteAnimationDuration;
    }
    public void Start()
    {
        _audioPlayer.OnTrackTimeChanged += ChechClosestNote;
        _audioPlayer.Play();
    }

    public void ChechClosestNote(float currentTime)
    {
        if (_currentNotes.TryPeek(out NoteData noteData))
        {
            if (noteData.PressTime < currentTime + _noteAnimationDuration)
            {
                _spawners[noteData.Line].SpawnNote(currentTime - noteData.PressTime);
                _currentNotes.Dequeue();
                ChechClosestNote(currentTime);
            }
        }
    }
}