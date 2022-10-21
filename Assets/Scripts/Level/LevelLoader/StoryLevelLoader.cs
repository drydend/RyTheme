using System;
using UnityEngine;
using Zenject;

public class StoryLevelLoader : MonoBehaviour
{
    private const string StoryPianoSceneName = "StoryPianoLevelScene";
    private const string StoryCrossSceneName = "StoryCrossLevelScene";

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
        var sceneName = GetSceneNameByLevelType(levelData.CurrentLevelType);
        var storyMode = new StoryGameMode(sceneName);

        _dataProvider.SetCurrentData(storyLevel, levelData);

        _gameModeMenager.SwitchMode(storyMode);
    }

    private string GetSceneNameByLevelType(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Piano:
                return StoryPianoSceneName;
            case LevelType.Cross:
                return StoryCrossSceneName;
            default:
                throw new Exception($"No scene for {levelType} level");
        }
    }
}