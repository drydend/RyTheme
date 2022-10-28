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
        _button.onClick.AddListener(_mainMenuLoader.LoadMainMenu);
    }
}
