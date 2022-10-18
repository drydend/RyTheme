using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StoryLevelInfoMenu : UIMenu
{
    private StoryLevel _currentStoryLevel;
    private LevelData _currentLevelData;
    private LevelType _selectedLevelType;

    [SerializeField]
    private Image _coverImage;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _author;
    [SerializeField]
    private TMP_Text _time;

    [SerializeField]
    private Button _playButton;
    private StoryLevelLoader _storyLevelLoader;

    [Inject]
    public void Constuct(StoryLevelLoader storyLevelLoader)
    {
        _storyLevelLoader = storyLevelLoader;
    }

    public void ShowLevel(StoryLevel storyLevel, LevelData levelData)
    {
        Open();
        _currentStoryLevel = storyLevel;
        _currentLevelData = levelData;

        _title.text = levelData.Title;
        _author.text = levelData.Artist;
        _time.text = "0.00 - " + GetTimeInMinutes(levelData.Music.length);
        _coverImage.sprite = levelData.Banner;
    }

    private void Awake()
    {
        _playButton.onClick.AddListener(PlayLevel);
    }

    private void PlayLevel()
    {
        _storyLevelLoader.PlayStoryLevel(_currentStoryLevel, _currentLevelData);
    }

    private string GetTimeInMinutes(float seconds)
    {
        int integerPart = Mathf.FloorToInt(seconds / 60);
        int floatPart = (int)seconds % 60;

        return $"{integerPart},{floatPart}";
    }
}
