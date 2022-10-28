using TMPro;
using UnityEngine;

public class ChainMenuPlaylistItem : ChainMenuItem
{
    private ParsedTrackData _parsedTrackData;
    private LevelData _levelData;

    private LevelType _levelType;

    [SerializeField]
    private TMP_Text _title;

    public LevelData LevelData
    {
        get
        {
            if(_levelData == null)
            {
                _levelData = new LevelDataLoader(_parsedTrackData, _levelType, LoadingSongType.Other).GetData();
            }
            
            return _levelData;
        }
    }

    public void Initialize(ParsedTrackData parsedTrackData, LevelType levelType)
    {   
        _parsedTrackData = parsedTrackData;
        _title.text = parsedTrackData.Title;
    }
}
