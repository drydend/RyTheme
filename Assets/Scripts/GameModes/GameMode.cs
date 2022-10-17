using System.Collections;
using UnityEngine.SceneManagement;

public abstract class GameMode
{
    protected readonly string _sceneName;

    public GameMode(string sceneName)
    {
        _sceneName = sceneName;
    }

    public abstract IEnumerator OnEnter();
    public abstract IEnumerator OnExit();
}
