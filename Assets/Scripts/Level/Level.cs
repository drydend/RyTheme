using System;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private AudioPlayer _audioPlayer;
    private StoryLevel _storyLevel;
    private LevelData _levelData;
    private NotesProvider _notesProvider;

    private LevelType _currentLevelType;
    private List<NoteSpawner> _spawners;
    private LevelHeals _heals;
    private LevelScore _levelScore;

    private Queue<NoteData> _currentNotes;
    private float _noteAnimationDuration;
    private GameConfig _gameConfig;

    public event Action OnLost;
    public event Action OnWin;

    public Level(AudioPlayer audioPlayer, StoryLevel track, LevelData levelData, LevelHeals levelHeals, LevelScore levelScore,
        NotesProvider notesProvider, List<NoteSpawner> spawners, GameConfig gameConfig)
    {
        _audioPlayer = audioPlayer;
        _storyLevel = track;
        _levelData = levelData;
        _heals = levelHeals;
        _levelScore = levelScore;
        _spawners = spawners;
        _gameConfig = gameConfig;
        _notesProvider = notesProvider;

        _currentNotes = levelData.LevelsPattern;
        _currentLevelType = levelData.CurrentLevelType;
        _noteAnimationDuration = gameConfig.NoteTimeToCrossing;
    }

    public void Start()
    {
        _heals.OnCurrentHealsIsZero += OnLoseLevel;
        _audioPlayer.OnTrackTimeChanged += TrySpawnClosestNote;
        _audioPlayer.OnTrackTimeChanged += CheckWinCondition;
        _audioPlayer.Play();
    }

    public void Stop()
    {
        _audioPlayer.OnTrackTimeChanged -= TrySpawnClosestNote;
        _audioPlayer.OnTrackTimeChanged -= CheckWinCondition;
        _audioPlayer.Stop();
    }

    public void TrySpawnClosestNote(float currentTime)
    {
        if (_currentNotes.TryPeek(out NoteData noteData))
        {
            if (noteData.PressTime < currentTime + _noteAnimationDuration)
            {
                _spawners[noteData.Line].SpawnNote(currentTime - noteData.PressTime);
                _currentNotes.Dequeue();
                TrySpawnClosestNote(currentTime);
            }
        }
    }

    private void CheckWinCondition(float songTime)
    {
        if(_currentNotes.Count == 0 && _notesProvider.IsThereNotesOnScene == false)
        {
            OnWinlevel();
        }
    }

    private void OnWinlevel()
    {
        OnWin?.Invoke();
        Stop();
    }

    private void OnLoseLevel()
    {
        OnLost?.Invoke();
        Stop();
    }
}