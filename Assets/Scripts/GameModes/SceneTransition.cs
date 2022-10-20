using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    private const string EnterAnimationTrigger = "Enter animation trigger";
    private const string ExitAnimationTrigger = "Exit animation trigger";

    public static SceneTransition Current { get; private set; }

    private Animator _animator;

    public IEnumerator PlayEnterAnimation()
    {
        _animator.SetTrigger(EnterAnimationTrigger);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
    
    public IEnumerator PlayExitAnimation()
    {
        _animator.SetTrigger(ExitAnimationTrigger);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    private void OnEnable()
    {   
        Current = this;
        _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        Current = null;
    }
}
