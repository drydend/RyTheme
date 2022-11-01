using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Input binds", fileName = "Input binds")]
public class InputBinds : ScriptableObject
{
    [Header("Piano")]
    [SerializeField]
    private KeyCode _leftPianoPartFirst = KeyCode.F;
    [SerializeField]
    private KeyCode _leftPianoPartCentre = KeyCode.D;
    [SerializeField]
    private KeyCode _leftPianoPartLast = KeyCode.S;
    [SerializeField]
    private KeyCode _rightPianoPartFirst = KeyCode.J;
    [SerializeField]
    private KeyCode _rightPianoPartCentre = KeyCode.K;
    [SerializeField]
    private KeyCode _rightPianoPartLast = KeyCode.L;

    [Space]
    [Header("UI")]
    [SerializeField]
    private KeyCode _pauseButton;

    public KeyCode PauseButton => _pauseButton;

    public KeyCode GetPianoButtonKeyCode(PianoButtonsPosition pianoButtonPosition)
    {
        switch (pianoButtonPosition)
        {
            case PianoButtonsPosition.LeftPartFirst:
                return _leftPianoPartFirst;
            case PianoButtonsPosition.LeftPartCenter:
                return _leftPianoPartCentre;
            case PianoButtonsPosition.LeftPartLast:
                return _leftPianoPartLast;
            case PianoButtonsPosition.RightPartFirst:
                return _rightPianoPartFirst;
            case PianoButtonsPosition.RightPartCenter:
                return _rightPianoPartCentre;
            case PianoButtonsPosition.RightPartLast:
                return _rightPianoPartLast;
            default:
                throw new Exception($"No bindings for {pianoButtonPosition}");
        }
    }
}