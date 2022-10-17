using Zenject;

public class StoryModeDataProviderInstaller : MonoInstaller
{
    private StoryModeDataProvider _provider;

    public override void InstallBindings()
    {   
        _provider = new StoryModeDataProvider();

        Container.Bind<StoryModeDataProvider>()
            .FromInstance(_provider)
            .AsSingle();
    }
}
