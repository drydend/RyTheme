using Zenject;

public class PauseHandlerInstaller : MonoInstaller
{
    private PauseHandle _pauseHandle;

    public override void InstallBindings()
    {
        _pauseHandle = new PauseHandle();

        Container.Bind<PauseHandle>()
            .FromInstance(_pauseHandle)
            .AsSingle();
    }
}
