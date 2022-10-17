using System;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private bool _coverPreviousMenu = true;

    public event Action OnOpened;
    public event Action OnClosed;
        
    public bool CoverPreviousMenu => _coverPreviousMenu;

    public void Open()
    {
        _menu.SetActive(true);
        OnOpened?.Invoke();
    }
    public void Close()
    {
        _menu.SetActive(false);
        OnClosed?.Invoke();
    }

    public void OnCovered()
    {
        _menu.SetActive(false);
    }

    public void OnUncovered()
    {
        _menu.SetActive(true);
    }
}
