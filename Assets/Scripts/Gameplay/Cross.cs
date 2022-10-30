using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Cross : MonoBehaviour
{
    private const string PressAnimation = "Press";

    [SerializeField]
    private PianoButtonsPosition _buttonPosition;

    private PlayerInput _input;
    private Animator _animator;

    public event Action OnPressed;

    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        _input = playerInput;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input.PianoButtonsEvents[_buttonPosition] += OnThisButtonDown;
    }

    private void OnThisButtonDown()
    {
        _animator.Play(PressAnimation);
        OnPressed();
    }
}
