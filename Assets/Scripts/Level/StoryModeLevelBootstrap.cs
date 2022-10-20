using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class StoryModeLevelBootstrap : LevelBootstrap
{
    private StoryModeDataProvider _dataProvider;

    private LevelScore _levelScore;
    private LevelHeals _levelHeals;
    private NotesProvider _notesProvider;

    private Level _currentLevel;

    [SerializeField]
    private LevelStartClock _startClock;

    [SerializeField]
    private GameConfig _config;

    [SerializeField]
    private AudioPlayer _audioPlayer;
    [SerializeField]
    private List<NotesLine> _notesLines;
    [SerializeField]
    private Note _notePrefab;

    [SerializeField]
    private LoseScrene _loseScrene;
    [SerializeField]
    private WinScrene _winScrene;

    private List<NoteSpawner> _notesSpawners = new List<NoteSpawner>();

    [Inject]
    public void Contruct(StoryModeDataProvider dataProvider, LevelHeals levelHeals, LevelScore levelScore, NotesProvider notesProvider)
    {
        _dataProvider = dataProvider;
        _notesProvider = notesProvider;
        _levelHeals = levelHeals;
        _levelScore = levelScore;
    }

    private void Awake()
    {
        _levelHeals.Initialize(_notesProvider, _config);
        _levelScore.Initialize(_notesProvider, _config);

        _audioPlayer.Initialize(_config.NoteTimeToCrossing, _dataProvider.CurrentLevelData);

        foreach (var line in _notesLines)
        {
            _notesSpawners.Add(new NoteSpawner(line, _notesProvider, _notePrefab, _config.NoteTimeToCrossing));
        }
    }

    private void Start()
    {
        _currentLevel = new Level(_audioPlayer, _dataProvider.CurrentStoryLevel, _dataProvider.CurrentLevelData,
            _levelHeals, _levelScore, _notesProvider ,_notesSpawners, _config);

        _currentLevel.OnLost += _loseScrene.Open;
        _currentLevel.OnWin += _winScrene.Open;

        StartCoroutine(StartLevel(_currentLevel));
    }

    private IEnumerator StartLevel(Level level)
    {
        yield return _startClock.StartClock();
        level.Start();
    }
}
