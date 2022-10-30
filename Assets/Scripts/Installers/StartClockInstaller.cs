using UnityEngine;
using Zenject;

public class StartClockInstaller : MonoInstaller
{
    [SerializeField]
    private LevelStartClock _startClock;

    public override void InstallBindings()
    {
        Container.Bind<LevelStartClock>()
            .FromInstance(_startClock)
            .AsSingle();
    }
}
