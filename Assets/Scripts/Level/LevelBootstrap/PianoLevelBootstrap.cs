using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PianoLevelBootstrap : MonoBehaviour
{
    protected NotesProvider _notesProvider;
    protected LevelScore _levelScore;
    protected LevelHeals _levelHeals;
    protected LevelStartClock _startClock;

    [SerializeField]
    protected GameConfig _config;
    
    [SerializeField]
    protected AudioPlayer _audioPlayer;


    [SerializeField]
    protected List<NotesLine> _notesLines;
    [SerializeField]
    protected Note _notePrefab;

    protected List<NoteSpawner> _notesSpawners = new List<NoteSpawner>();

    [SerializeField]
    private LevelUI _levelUi;
    [SerializeField]
    protected LoseScrene _loseScrene;
    [SerializeField]
    protected WinScrene _winScrene;
    protected Level _currentLevel;

    private LevelDataProvider _dataProvider;
    [Inject]
    public void Contruct(LevelDataProvider dataProvider, LevelHeals levelHeals,
        LevelScore levelScore, NotesProvider notesProvider, LevelStartClock startClock)
    {
        _dataProvider = dataProvider;
        _levelHeals = levelHeals;
        _levelScore = levelScore;
        _notesProvider = notesProvider;
        _startClock = startClock;
    }

    protected virtual void CreateLevel()
    {
        _currentLevel = new Level(_audioPlayer, _dataProvider.CurrentLevelData,
        _levelHeals, _levelScore, _notesProvider, _notesSpawners, _config);

        _currentLevel.OnLost += OnLost;
        _currentLevel.OnWin += OnWin;
    }

    protected IEnumerator StartLevel(Level level)
    {
        yield return _startClock.StartClock();
        level.Start();
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
        CreateLevel();

        StartCoroutine(StartLevel(_currentLevel));
    }

    protected virtual void OnWin()
    {
        _winScrene.Show();

        foreach (var line in _notesLines)
        {
            line.gameObject.SetActive(false);
        }
    }

    protected virtual void OnLost()
    {
        _loseScrene.Open();

        foreach (var line in _notesLines)
        {
            line.gameObject.SetActive(false);
        }
    }
}