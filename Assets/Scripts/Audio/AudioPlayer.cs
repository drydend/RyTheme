using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour 
{
    private AudioSource _audioSource;

    private AudioClip _currentSong;
    private LevelData _levelData;
    private float _noteAnimationDuration;

    public event Action<float> OnTrackTimeChanged;

    public void Initialize(float noteAnimationDuration, LevelData levelData)
    {
        _noteAnimationDuration = noteAnimationDuration;
        _levelData = levelData;
        _currentSong = levelData.Music;
        _audioSource.clip = _currentSong;
    }

    public void Play()
    {
        StartCoroutine(SongCoroutine());
    }

    public IEnumerator SongCoroutine()
    {
        float timeElapsed = -_levelData.SongTimeOffset;

        while(_noteAnimationDuration > timeElapsed)
        {
            OnTrackTimeChanged?.Invoke(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _audioSource.Play();

        var timeOffset = _noteAnimationDuration - timeElapsed ;

        while(_currentSong.length > _audioSource.time + timeOffset + _noteAnimationDuration)
        {
            OnTrackTimeChanged?.Invoke(_audioSource.time + timeElapsed + timeOffset);
            yield return null;
        }
    }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
