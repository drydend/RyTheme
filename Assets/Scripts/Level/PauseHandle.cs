using UnityEngine;

public class PauseHandle
{
    public bool IsPaused { get; private set; }

    private float _previousTimeScale;

    public void Pause()
    {
        if (IsPaused)
        {
            return;
        }

        IsPaused = true;

        _previousTimeScale =  Time.timeScale;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        if (!IsPaused)
        {
            return;
        }

        IsPaused = false;
        Time.timeScale = _previousTimeScale;
    }
}