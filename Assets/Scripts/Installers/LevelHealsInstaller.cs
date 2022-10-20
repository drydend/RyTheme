using UnityEngine;
using Zenject;

public class LevelHealsInstaller : MonoInstaller
{
    [SerializeField]
    private LevelHeals _levelHeals;

    public override void InstallBindings()
    {
        Container.Bind<LevelHeals>()
            .FromInstance(_levelHeals)
            .AsSingle();
    }
}
