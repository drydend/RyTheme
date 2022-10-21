using UnityEngine;
using Zenject;

public class GameModeMenagerInstaller : MonoInstaller
{
    [SerializeField]
    private GameModeManager _gameModeMenager;

    public override void InstallBindings()
    {   
        var gameModeMenager = Instantiate(_gameModeMenager);
        DontDestroyOnLoad(gameModeMenager.gameObject);

        Container.Bind<GameModeManager>()
            .FromInstance(gameModeMenager)
            .AsSingle();
    }
}
