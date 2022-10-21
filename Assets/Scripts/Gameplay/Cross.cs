using System;
using UnityEngine;
using Zenject;

public class Cross : MonoBehaviour
{
    [SerializeField]
    private PianoButtonsPosition _buttonPosition;

    private PlayerInput _input;

    public event Action OnPressed;

    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        _input = playerInput;
    }

    private void Awake()
    {
        _input.PianoButtonsEvents[_buttonPosition] += OnThisButtonDown;
    }

    private void OnThisButtonDown()
    {
        Debug.Log($"Pressed {_buttonPosition}");
        OnPressed();
    }
}
