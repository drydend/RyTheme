using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LevelBootstrap : MonoBehaviour
{
    protected StoryModeDataProvider _dataProvider;

    protected LevelScore _levelScore;
    protected LevelHeals _levelHeals;

    [SerializeField]
    protected GameConfig _config;
    
    [SerializeField]
    protected LevelStartClock _startClock;
    [SerializeField]
    protected AudioPlayer _audioPlayer;

    protected Level _currentLevel;

    [SerializeField]
    protected LoseScrene _loseScrene;
    [SerializeField]
    protected WinScrene _winScrene;

    [Inject]
    public void Contruct(StoryModeDataProvider dataProvider, LevelHeals levelHeals, LevelScore levelScore)
    {
        _dataProvider = dataProvider;
        _levelHeals = levelHeals;
        _levelScore = levelScore;
    }

    protected IEnumerator StartLevel(Level level)
    {
        yield return _startClock.StartClock();
        level.Start();
    }
}