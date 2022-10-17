using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChainMenu<ItemType> where ItemType : ChainMenuItem
{
    private float _distanceBetweenItems = 200;
    private int _startItemIndex;
    private RectTransform _itemsParent;

    private List<ItemType> _items;
    private int _selectedItemIndex = 0;

    public RectTransform ItemsParent => _itemsParent;
    public ItemType SelectedItem { get; set; }

    public ChainMenu(List<ItemType> chainMenuItems, float distanceBetweenItems, 
        int startItemIndex, RectTransform itemsParent)
    {
        _items = chainMenuItems;
        _distanceBetweenItems = distanceBetweenItems;
        _startItemIndex = startItemIndex;
        _itemsParent = itemsParent;
    }

    public void SwitchItemsPosition(ChainMenuDirection direction)
    {
        switch (direction)
        {
            case ChainMenuDirection.Right:
                SwitchToRightItem();
                break;
            case ChainMenuDirection.Left:
                SwitchToLeftItem();
                break;
            default:
                break;
        }
    }

    public void SetItemsStartPosition()
    {
        _selectedItemIndex = _startItemIndex;
        SelectedItem = _items[_selectedItemIndex];
        SelectedItem.OnSelected();

        SelectedItem.RectTransform.anchoredPosition = _itemsParent.anchoredPosition;
        for (int i = 0; i < _items.Count; i++)
        {
            var currentItem = _items[i];

            if (currentItem == SelectedItem)
            {
                continue;
            }

            var distanceFromSelectedItem = _distanceBetweenItems * (i - _selectedItemIndex);
            var itemPosition = new Vector2(SelectedItem.RectTransform.anchoredPosition.x + distanceFromSelectedItem,
                SelectedItem.RectTransform.anchoredPosition.y);

            currentItem.RectTransform.anchoredPosition = itemPosition;
        }
    }

    private void SwitchToLeftItem()
    {
        if (_selectedItemIndex == 0)
        {
            return;
        }

        SelectedItem.OnUnselected();
        _selectedItemIndex--;

        SelectedItem = _items[_selectedItemIndex];
        SelectedItem.OnSelected();

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].RectTransform.anchoredPosition =
                new Vector2(_items[i].RectTransform.anchoredPosition.x + _distanceBetweenItems,
                _items[i].RectTransform.anchoredPosition.y);
        }

    }

    private void SwitchToRightItem()
    {
        if (_selectedItemIndex == _items.Count - 1)
        {
            return;
        }

        SelectedItem.OnUnselected();
        _selectedItemIndex++;

        SelectedItem = _items[_selectedItemIndex];
        SelectedItem.OnSelected();

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].RectTransform.anchoredPosition =
                new Vector2(_items[i].RectTransform.anchoredPosition.x - _distanceBetweenItems,
                _items[i].RectTransform.anchoredPosition.y);
        }
    }
}
