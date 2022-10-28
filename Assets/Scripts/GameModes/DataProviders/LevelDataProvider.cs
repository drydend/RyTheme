using Unity.VisualScripting.FullSerializer;

public class LevelDataProvider
{
    private LevelData _currentLevelData;

    protected bool _hasData;

    public LevelData CurrentLevelData
    {   
        get
        {
            if (_hasData)
            {
                return _currentLevelData;
            }

            throw new System.Exception("Story mode data provider has no to data to get");
        }
    }


    public void SetCurrentLevelData(LevelData levelData)
    {
        _hasData = true;
        _currentLevelData = levelData;
    }
}