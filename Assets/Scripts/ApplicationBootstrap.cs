using UnityEngine;
using Zenject;

public class ApplicationBootstrap : MonoBehaviour
{
    private const string MainMenuScene = "MainMenu";

    private GameModeManager _gameModeManager;

    [Inject]
    public void Construct(GameModeManager gameModeManager)
    {
        _gameModeManager = gameModeManager;
    }

    private void Awake()
    {
        var mainMenuMode = new MainMenuMode(MainMenuScene);
        _gameModeManager.EnterStartMode(mainMenuMode);
    }
}
