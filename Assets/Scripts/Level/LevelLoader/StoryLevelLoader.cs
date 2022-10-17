using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StoryLevelLoader : MonoBehaviour
{
    private const string StorySceneName = "StoryGameModeScene";

    private GameModeManager _gameModeMenager;
    private StoryModeDataProvider _dataProvider;

    [Inject]
    public void Construct(StoryModeDataProvider dataProvider, GameModeManager gameModeMenager)
    {
        _gameModeMenager = gameModeMenager;
        _dataProvider = dataProvider;
    }

    public void PlayStoryLevel(StoryLevel storyLevel, LevelData levelData)
    {
        var storyMode = new StoryGameMode(StorySceneName);
        _dataProvider.SetCurrentData(storyLevel, levelData);

        _gameModeMenager.SwitchMode(storyMode);
    }
}