using UnityEngine;
using Zenject;

public class MainMenuLoader : MonoBehaviour
{
    private const string MainMenuScene = "MainMenuScene";
    private GameModeManager _gameModeManager;

    [Inject]
    public void Construct(GameModeManager gameModeManager)
    {
        _gameModeManager = gameModeManager;
    }

    public void LoadMainMenu()
    { 
        var mainMenuMode = new MainMenuMode(MainMenuScene);

        _gameModeManager.SwitchMode(mainMenuMode);
    }
}