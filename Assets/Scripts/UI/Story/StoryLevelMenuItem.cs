using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class StoryLevelMenuItem : MonoBehaviour
{
    private StoryLevelMenuItem _nextItem;
    private StoryLevelInfoMenu _menu;

    private Image _image;
    private Button _button;
    public StoryLevel StoryLevel { get; private set; }
    public LevelData LevelData { get; private set; }

    public void Initialize(StoryLevel storyLevel, LevelData levelData, StoryLevelInfoMenu storyLevelInfoMenu)
    {
        StoryLevel = storyLevel;
        LevelData = levelData;
        _menu = storyLevelInfoMenu;

        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _image.sprite = levelData.Banner;
        _button.onClick.AddListener(() => _menu.ShowLevel(StoryLevel, LevelData));
    }

    public void SetNextItem(StoryLevelMenuItem menuItem)
    {
        _nextItem = menuItem;
    }
}