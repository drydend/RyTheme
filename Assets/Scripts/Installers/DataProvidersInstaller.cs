using Zenject;

public class DataProvidersInstaller : MonoInstaller
{
    private StoryLevelDataProvider _storyLevelDataprovider;
    private LevelDataProvider _levelDataprovider;

    public override void InstallBindings()
    {
        BindLevelDataProvider();
        BindStoryLevelDataProvider();
    }

    private void BindStoryLevelDataProvider()
    {
        _storyLevelDataprovider = new StoryLevelDataProvider();

        Container.Bind<StoryLevelDataProvider>()
            .FromInstance(_storyLevelDataprovider)
            .AsSingle();
    }

    private void BindLevelDataProvider()
    {
        _levelDataprovider = new LevelDataProvider();

        Container.Bind<LevelDataProvider>()
            .FromInstance(_levelDataprovider)
            .AsSingle();
    }
}
