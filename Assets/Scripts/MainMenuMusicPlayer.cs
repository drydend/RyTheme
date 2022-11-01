using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _audioClip;

    public void Play()
    {
        _audioSource.PlayOneShot(_audioClip); 
    }
}
