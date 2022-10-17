using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuHolder : MonoBehaviour
{
    [SerializeField]
    private List<UIMenu> _allMenus;
    [SerializeField]
    private UIMenu _startMenu;

    private Stack<UIMenu> _openedMenus = new Stack<UIMenu>();
    private void Awake()
    {
        foreach (var menu in _allMenus)
        {
            menu.Close();
            menu.OnOpened += () => OnMenuOpened(menu);
            menu.OnClosed += OnMenuClosed;
        }

        _startMenu.Open();
    }

    private void OnMenuOpened(UIMenu menu)
    {
        if (menu.CoverPreviousMenu && _openedMenus.Count > 0)
        {
            _openedMenus.Peek().OnCovered();
        }

        _openedMenus.Push(menu);
    }

    private void OnMenuClosed()
    {
        _openedMenus.Pop();
        _openedMenus?.Peek().OnUncovered();
    }
}
