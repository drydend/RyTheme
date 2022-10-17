﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterMenuProvider : MonoBehaviour
{
    [SerializeField]
    private List<ChainMenuButton> _buttons;
    [SerializeField]
    private Button _choseButton;
    [SerializeField]
    private StoryLevelsMenu _storylevelMenu;

    private ChainMenu<ChainMenuStoryChapterItem> _chainMenu;
    public void Initialize(ChainMenu<ChainMenuStoryChapterItem> chainMenu)
    {
        _chainMenu = chainMenu;

        foreach (var button in _buttons)
        {
            button.OnPressed += (direction) => _chainMenu.SwitchItemsPosition(direction);
        }

        _choseButton.onClick.AddListener(() => _storylevelMenu.ShowLevels(_chainMenu.SelectedItem.Chapter));
    }
}