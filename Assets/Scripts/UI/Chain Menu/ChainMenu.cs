using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChainMenu<ItemType> where ItemType : ChainMenuItem
{ 
    private Vector2 _itemsDirection;
    private float _distanceBetweenItems = 200;
    private int _startItemIndex;
    private RectTransform _itemsParent;

    private List<ItemType> _items;
    private int _selectedItemIndex = 0;

    public RectTransform ItemsParent => _itemsParent;
    public ItemType SelectedItem { get; set; }

    public ChainMenu(List<ItemType> chainMenuItems, float distanceBetweenItems, 
        int startItemIndex, RectTransform itemsParent, Vector2 itemsDirection)
    {
        _items = chainMenuItems;
        _distanceBetweenItems = distanceBetweenItems;
        _startItemIndex = startItemIndex;
        _itemsParent = itemsParent;
        _itemsDirection = itemsDirection;
        _itemsDirection.Normalize();
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

        SelectedItem.RectTransform.anchoredPosition = Vector2.zero;
        for (int i = 0; i < _items.Count; i++)
        {
            var currentItem = _items[i];

            if (currentItem == SelectedItem)
            {
                continue;
            }

            var distanceFromSelectedItem = _distanceBetweenItems * (i - _selectedItemIndex);
            var itemPosition = _itemsDirection * distanceFromSelectedItem;

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
            _items[i].RectTransform.anchoredPosition = _items[i].RectTransform.anchoredPosition + _itemsDirection * _distanceBetweenItems;

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
            _items[i].RectTransform.anchoredPosition = _items[i].RectTransform.anchoredPosition - _itemsDirection * _distanceBetweenItems;
        }
    }
}
