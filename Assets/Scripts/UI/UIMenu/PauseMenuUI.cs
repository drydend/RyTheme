using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

public class PauseMenuUI : UIMenu
{
    private PauseHandle _pauseHandler;
    private PlayerInput _playerInput;

    private bool _isOpened;

    [Inject]
    public void Construct(PlayerInput playerInput, PauseHandle pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _playerInput = playerInput;
    }

    private void Awake()
    {
        _playerInput.OnPauseButtonPressed += OnPauseButtonPressed;
    }

    private void OnPauseButtonPressed()
    {
        if (_isOpened)
        {
            Close();
            UnPauseGame();
            _isOpened = false;
        }
        else
        {
            Open();
            PauseGame();
            _isOpened = true;
        }
    }

    private void PauseGame()
    {
        _pauseHandler.Pause();
    }

    private void UnPauseGame()
    {
        _pauseHandler.UnPause();
    }
}
