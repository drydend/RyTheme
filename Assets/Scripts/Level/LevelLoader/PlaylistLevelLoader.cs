using System;
using UnityEngine;
using Zenject;

public class PlaylistLevelLoader : MonoBehaviour
{
    private const string PlaylistCrossLevelScene = "PlaylistCrossLevelScene";
    private const string PlaylistPianoLevelScene = "PlaylistPianoLevelScene";

    private LevelDataProvider _dataProvider;   
    private GameModeManager _gameModeManager;

    [Inject]
    public void Contruct(LevelDataProvider levelDataProvider, GameModeManager gameModeManager)
    {
        _dataProvider = levelDataProvider;
        _gameModeManager = gameModeManager;
    }

    public void PlayPlaylistLevel(LevelData levelData)
    {
        var sceneName = GetSceneNameByLevelType(levelData.CurrentLevelType);
        var storyMode = new PlaylistGameMode(sceneName);

        _dataProvider.SetCurrentLevelData(levelData);

        _gameModeManager.SwitchMode(storyMode);
    }

    private string GetSceneNameByLevelType(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Piano:
                return PlaylistPianoLevelScene;
            case LevelType.Cross:
                return PlaylistCrossLevelScene;
            default:
                throw new Exception($"No scene for {levelType} level");
        }
    }
}
