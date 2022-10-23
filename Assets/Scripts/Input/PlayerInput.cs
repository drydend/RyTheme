using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private InputBinds _inputBinds;

    public event Action OnLeftPianoPartFirstPressed;
    public event Action OnLeftPianoPartCenterPressed;
    public event Action OnLeftPianoPartLastPressed;
    
    public event Action OnRightPianoPartFirstPressed;
    public event Action OnRightPianoPartCenterPressed;
    public event Action OnRightPianoPartLastPressed;

    public event Action OnMouseSrollDeltaChanged;

    public Dictionary<PianoButtonsPosition, Action> PianoButtonsEvents { get; private set; }
    public float MouseScrolDelta => Input.mouseScrollDelta.y;

    private void Awake()
    {
        PianoButtonsEvents = new Dictionary<PianoButtonsPosition, Action>();

        PianoButtonsEvents[PianoButtonsPosition.LeftPartFirst] = OnLeftPianoPartFirstPressed;
        PianoButtonsEvents[PianoButtonsPosition.LeftPartCenter] = OnLeftPianoPartCenterPressed;
        PianoButtonsEvents[PianoButtonsPosition.LeftPartLast] = OnLeftPianoPartLastPressed;
        PianoButtonsEvents[PianoButtonsPosition.RightPartFirst] = OnRightPianoPartFirstPressed;
        PianoButtonsEvents[PianoButtonsPosition.RightPartCenter] = OnRightPianoPartCenterPressed;
        PianoButtonsEvents[PianoButtonsPosition.RightPartLast] = OnRightPianoPartLastPressed;
    }

    private void Update()
    {
        foreach (PianoButtonsPosition item in Enum.GetValues(typeof(PianoButtonsPosition)))
        {
            if (Input.GetKeyDown(_inputBinds.GetPianoButtonKeyCode(item)))
            {
                PianoButtonsEvents[item]?.Invoke();
            }
        }
        
        if(Input.mouseScrollDelta.y != 0)
        {
            OnMouseSrollDeltaChanged?.Invoke();
        }
    }
}
