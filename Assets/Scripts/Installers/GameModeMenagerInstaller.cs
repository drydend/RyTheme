using UnityEngine;
using Zenject;

public class GameModeMenagerInstaller : MonoInstaller
{
    [SerializeField]
    private GameModeManager _gameModeMenager;

    public override void InstallBindings()
    {
        Container.Bind<GameModeManager>()
            .FromComponentInNewPrefab(_gameModeMenager)
            .AsSingle();
    }
}
