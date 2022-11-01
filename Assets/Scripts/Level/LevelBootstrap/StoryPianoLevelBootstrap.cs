using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoryPianoLevelBootstrap : PianoLevelBootstrap
{
    private StoryLevelDataProvider _storyLevelDataProvider;

    [Inject]
    public void Contruct(NotesProvider notesProvider, StoryLevelDataProvider dataProvider)
    {
        _storyLevelDataProvider = dataProvider;
    }

    private void Awake()
    {
        _levelHeals.Initialize(_notesProvider, _config);
        _levelScore.Initialize(_notesProvider, _config);

        _audioPlayer.Initialize(_config.NoteTimeToCrossing, _storyLevelDataProvider.CurrentLevelData);

        foreach (var line in _notesLines)
        {
            _notesSpawners.Add(new NoteSpawner(line, _notesProvider, _notePrefab, _config.NoteTimeToCrossing));
        }
    }

    private void Start()
    {
        CreateLevel();

        StartCoroutine(StartLevel(_currentLevel));
    }

    protected override void CreateLevel()
    {
        _currentLevel = new Level(_audioPlayer, _storyLevelDataProvider.CurrentLevelData,
        _levelHeals, _levelScore, _notesProvider, _notesSpawners, _config);

        _currentLevel.OnLost += OnLost;
        _currentLevel.OnWin += OnWin;
    }
}
