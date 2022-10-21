using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoryPianoLevelBootstrap : LevelBootstrap
{
    private NotesProvider _notesProvider;

    [SerializeField]
    private List<NotesLine> _notesLines;
    [SerializeField]
    private Note _notePrefab;

    private List<NoteSpawner> _notesSpawners = new List<NoteSpawner>();

    [Inject]
    public void Contruct(NotesProvider notesProvider)
    {
        _notesProvider = notesProvider;
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
}
