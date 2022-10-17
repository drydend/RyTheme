using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class StoryModeLevelBootstrap : LevelBootstrap
{   
    private StoryModeDataProvider _dataProvider;

    [SerializeField]
    private GameConfig _config;

    [SerializeField]
    private AudioPlayer _audioPlayer;
    [SerializeField]
    private List<NotesLine> _notesLines;
    [SerializeField]
    private Note _notePrefab;

    private Level _currentLevel;

    private List<NoteSpawner> _notesSpawners = new List<NoteSpawner>();

    [Inject]
    public void Contruct(StoryModeDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    private void Start()
    {   

        foreach (var line in _notesLines)
        {
            _notesSpawners.Add(new NoteSpawner(line, _notePrefab, _config.NoteTimeToCrossing));
        }

        _audioPlayer.Initialize(_config.NoteTimeToCrossing, _dataProvider.CurrentLevelData);

        _currentLevel = new Level(_audioPlayer, _dataProvider.CurrentStoryLevel, _dataProvider.CurrentLevelData,
            _notesSpawners, _config.NoteTimeToCrossing);
        
        _currentLevel.Start();
    }
}
