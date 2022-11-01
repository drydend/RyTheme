using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    private Button _button;

    [SerializeField]
    private MainMenuLoader _mainMenuLoader;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        _mainMenuLoader.LoadMainMenu();
    }
}
