using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChainMenuPlaylistItem : ChainMenuItem
{
    [SerializeField]
    private Image _bannerImage;
    [SerializeField]
    private TMP_Text _artistText;
    [SerializeField]
    private TMP_Text _difficulty;

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
        _bannerImage.sprite = LevelData.Banner;
        _artistText.text = LevelData.Artist;
        _difficulty.text = LevelData.LevelDifficulty.ToString();
        _title.text = parsedTrackData.Title;
        _levelType = levelType;
    }
}
