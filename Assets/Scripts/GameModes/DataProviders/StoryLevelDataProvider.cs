using UnityEngine;

public class StoryLevelDataProvider : LevelDataProvider
{
    public StoryLevel _storyLevel;
    public StoryLevel CurrentStoryLevel
    {
        get
        {
            if (_storyLevel)
            {
                return _storyLevel;
            }

            throw new System.Exception("Story mode data provider has no to data to get");
        }
    }

    public void SetCurrentStoryLevelData(LevelData levelData, StoryLevel storyLevel)
    {
        _storyLevel = storyLevel;
        SetCurrentLevelData(levelData);
    }
}
