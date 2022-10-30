using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChapterMenuBootstrap : MonoBehaviour
{
    [SerializeField]
    private List<StoryChapter> _storyChapters;
    [SerializeField]
    private ChainMenuStoryChapterItem _chainMenuItemPrefab;
    
    [SerializeField]
    private ChainMenuConfig _menuConfig;
    [SerializeField]
    private RectTransform _itemsParent;

    [SerializeField]
    private ChapterMenuHandler _chapterMenuProvider;

    private void Awake()
    { 
        _storyChapters.OrderBy(chapter => chapter.ChapterNumber);

        var chainMenuItems = new List<ChainMenuStoryChapterItem>();

        foreach (var chapter in _storyChapters)
        {
            var menuItem = Instantiate(_chainMenuItemPrefab, _itemsParent);
            chainMenuItems.Add(menuItem);
            menuItem.Initialize(chapter);
        }
        
        var menu = new ChainMenu<ChainMenuStoryChapterItem>(chainMenuItems, _itemsParent, _menuConfig);
        menu.SetItemsStartPosition();
        StartCoroutine(menu.PlaySwitchAnimationCoroutine());

        _chapterMenuProvider.Initialize(menu);
    }
}
