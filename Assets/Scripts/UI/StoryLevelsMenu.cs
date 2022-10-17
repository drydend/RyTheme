using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIMenu))]
public class StoryLevelsMenu : MonoBehaviour
{
    [SerializeField]
    private StoryLevelMenuItem _levelMenuItemPrefab;
    [SerializeField]
    private StoryLevelMenuItem _lastLevelMenuItemPrefab;

    [SerializeField]
    private RectTransform _itemParent;

    [SerializeField]
    private StoryLevelInfoMenu _storyLevelInfoMenu;

    private StoryChapter _shownChapter;
    private List<StoryLevelMenuItem> _currentLevelItems = new List<StoryLevelMenuItem>();
    private UIMenu _menu;

    private UIMenu Menu
    {
        get
        {
            if (_menu == null)
                _menu = GetComponent<UIMenu>();

            return _menu;
        }
    }

    public void ShowLevels(StoryChapter storyChapter)
    {
        Menu.Open();

        if (_shownChapter != null && _shownChapter == storyChapter)
        {
            return;
        }

        _shownChapter = storyChapter;
        DestroyCurrentMenuItems();

        var currentLevel = storyChapter.FirstLevel;

        if (storyChapter.FirstLevel != storyChapter.LastLevel)
        {
            var levelItem = Instantiate(_levelMenuItemPrefab, _itemParent);

            var levelDataLoader = new LevelDataLoader(currentLevel.Track.SMFileName, storyChapter.LevelType,
                LoadingSongType.Story);
            var levelData = levelDataLoader.GetData();

            levelItem.Initialize(currentLevel, levelData, _storyLevelInfoMenu);
            _currentLevelItems.Add(levelItem);

            while (currentLevel.NextLevel != null)
            {
                currentLevel = currentLevel.NextLevel;
                var nextlevelItem = Instantiate(_levelMenuItemPrefab, _itemParent);

                levelData = new LevelDataLoader(currentLevel.Track.SMFileName, storyChapter.LevelType,
                    LoadingSongType.Story).GetData();

                nextlevelItem.Initialize(currentLevel, levelData, _storyLevelInfoMenu); ;

                levelItem.SetNextItem(nextlevelItem);
                levelItem = nextlevelItem;
                _currentLevelItems.Add(levelItem);
            }

            var lastLevelItem = Instantiate(_lastLevelMenuItemPrefab, _itemParent);

            levelData = new LevelDataLoader(storyChapter.LastLevel.Track.SMFileName, storyChapter.LevelType,
                LoadingSongType.Story).GetData();

            lastLevelItem.Initialize(storyChapter.LastLevel, levelData, _storyLevelInfoMenu);
            levelItem.SetNextItem(lastLevelItem);
            _currentLevelItems.Add(lastLevelItem);
        }
        else
        {
            var lastLevelItem = Instantiate(_lastLevelMenuItemPrefab, _itemParent);

            var levelData = new LevelDataLoader(storyChapter.LastLevel.Track.SMFileName, storyChapter.LevelType,
                LoadingSongType.Story).GetData();

            lastLevelItem.Initialize(storyChapter.LastLevel, levelData, _storyLevelInfoMenu);
            _currentLevelItems.Add(lastLevelItem);
        }
    }

    private void DestroyCurrentMenuItems()
    {
        foreach (var item in _currentLevelItems)
        {
            Destroy(item.gameObject);
        }

        _currentLevelItems.Clear();
    }
}