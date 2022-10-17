public class StoryModeDataProvider
{
    public StoryLevel CurrentStoryLevel {get; private set;}
    public LevelData CurrentLevelData { get; private set;}
    
    public void SetCurrentData(StoryLevel storyLevel ,LevelData levelData)
    {
        CurrentStoryLevel = storyLevel;
        CurrentLevelData = levelData;
    }
}
