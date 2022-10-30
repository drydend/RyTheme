using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ChainMenu<ItemType> where ItemType : ChainMenuItem
{
    private Vector2 _itemsDirection;
    private float _distanceBetweenItems = 200;
    private int _startItemIndex;
    private RectTransform _itemsParent;

    private List<ItemType> _items;
    private List<Vector2> _itemsDisiredPosition;
    private int _selectedItemIndex = 0;
    private float _animationLerpFactor = 0.5f;
    private float _animationDurationFactor = 1f;

    public RectTransform ItemsParent => _itemsParent;
    public ItemType SelectedItem { get; set; }

    public ChainMenu(List<ItemType> chainMenuItems, RectTransform itemsParent, ChainMenuConfig chainMenuConfig)
    {
        _items = chainMenuItems;
        Vector2[] vector2s = new Vector2[chainMenuItems.Count];
        _itemsDisiredPosition = vector2s.ToList();
        _itemsParent = itemsParent;
        _distanceBetweenItems = chainMenuConfig.DistanceBetweenItems;
        _startItemIndex = chainMenuConfig.StartItemIndex;
        _itemsDirection = chainMenuConfig.ItemsPlacementDirection;
        _animationLerpFactor = chainMenuConfig.AnimationLerpFactor;
        _animationDurationFactor = chainMenuConfig.AnimationDurationFactor;
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
        _itemsDisiredPosition[_selectedItemIndex] = Vector2.zero;
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
            _itemsDisiredPosition[i] = itemPosition;
        }
    }

    public IEnumerator PlaySwitchAnimationCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].RectTransform.anchoredPosition =
                    Vector2.LerpUnclamped(_items[i].RectTransform.anchoredPosition, 
                    _itemsDisiredPosition[i], _animationLerpFactor * Time.deltaTime * _animationDurationFactor);
            }

            yield return null;
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
            _itemsDisiredPosition[i] = _itemsDisiredPosition[i] + _itemsDirection * _distanceBetweenItems;
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
            _itemsDisiredPosition[i] = _itemsDisiredPosition[i] - _itemsDirection * _distanceBetweenItems;
        }
    }
}
