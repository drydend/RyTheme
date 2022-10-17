using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    private GameMode _currentMode;

    private bool _isSwitching = false;
    private bool _isStarted = false;

    public void SwitchMode(GameMode mode)
    {
        StartCoroutine(SwitchModeCoroutine(mode));
    }

    public void EnterStartMode(GameMode mode)
    {
        if (_isStarted)
        {
            return;
        }
        
        _isStarted = true;
        StartCoroutine(EnterStartModeCoroutine(mode));
    }

    private IEnumerator EnterStartModeCoroutine(GameMode mode)
    {
        _isSwitching = true;

        _currentMode = mode;

        yield return _currentMode.OnEnter();

        yield return SceneTransition.Current.PlayEnterAnimation();

        _isSwitching = false;
    }

    private IEnumerator SwitchModeCoroutine(GameMode mode)
    {
        if (!_isStarted)
        {
            throw new Exception("Start Game mode is not entered");
        }

        yield return new WaitUntil(() => _isSwitching == false);

        if (_currentMode == mode)
        {
            yield break;
        }

        _isSwitching = true;

        yield return SceneTransition.Current.PlayExitAnimation();

        if (_currentMode != null)
        {
            yield return _currentMode.OnExit();
        }

        _currentMode = mode;

        yield return _currentMode.OnEnter();

        yield return SceneTransition.Current.PlayEnterAnimation();

        _isSwitching = false;
    }
}
