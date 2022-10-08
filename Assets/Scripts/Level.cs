using System.Collections.Generic;

public class Level
{
    private AudioPlayer _audioPlayer;
    private Track _track;

    private LevelPattern _levelPattern;
    private SupportedSongType _currentLevelType;
    private List<NoteSpawner> _spawners;

    private Queue<NoteData> _currentNotes;
    private float _noteAnimationDuration;
    public Level(AudioPlayer audioPlayer, Track track, LevelPattern levelPattern, 
        List<NoteSpawner> spawners,SupportedSongType currentLevelType,float noteAnimationDuration)
    {
        _audioPlayer = audioPlayer;
        _track = track;
        _levelPattern = levelPattern;
        _spawners = spawners;
        _currentLevelType = currentLevelType;
        _noteAnimationDuration = noteAnimationDuration;
    }
    public void Start()
    {
        _currentNotes = _levelPattern.Notes[_currentLevelType];
        _audioPlayer.OnTrackTimeChanged += ChechClosestNote;
        _audioPlayer.Play();
    }

    public void ChechClosestNote(float currentTime)
    {
        if (_currentNotes.TryPeek(out NoteData noteData))
        {
            if(noteData.PressTime < currentTime + _noteAnimationDuration)
            {
                _spawners[noteData.Line].SpawnNote(currentTime - noteData.PressTime);
                _currentNotes.Dequeue();
                ChechClosestNote(currentTime);
            }
        }
    }
}