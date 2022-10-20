public class StoryModeDataProvider
{
    private StoryLevel _currentStoryLevel;
    private LevelData _currentLevelData;

    private bool HasData;

    public StoryLevel CurrentStoryLevel 
    { 
        get 
        {
            if (HasData)
            {
                return _currentStoryLevel;
            }

            throw new System.Exception("Story mode data provider has no to data to get");
        }
    }
    public LevelData CurrentLevelData
    {
        get
        {
            if (HasData)
            {
                return _currentLevelData;
            }

            throw new System.Exception("Story mode data provider has no to data to get");
        }
    }


    public void SetCurrentData(StoryLevel storyLevel ,LevelData levelData)
    {   
        HasData = true;
        _currentStoryLevel = storyLevel;
        _currentLevelData = levelData;
    }
}
