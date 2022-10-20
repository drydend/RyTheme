using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuMode : GameMode
{
    public MainMenuMode(string sceneName) : base(sceneName)
    {
    }

    public override IEnumerator OnEnter()
    {
        yield return SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
    }

    public override IEnumerator OnExit()
    {
        yield return SceneManager.UnloadSceneAsync(_sceneName);
    }
}
