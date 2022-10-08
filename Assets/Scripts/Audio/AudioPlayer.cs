using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour 
{
    private AudioSource _audioSource;

    private Track _track;
    private TrackData _trackData;
    private float _noteAnimationDuration;

    public event Action<float> OnTrackTimeChanged;

    public void Initialize(float noteAnimationDuration, Track track, TrackData trackData)
    {
        _noteAnimationDuration = noteAnimationDuration;
        _track = track;
        _trackData = trackData;

        _audioSource.clip = _track.CurrentTrack;
    }

    public void Play()
    {
        StartCoroutine(SongCoroutine());
    }

    public IEnumerator SongCoroutine()
    {
        float timeElapsed = -_trackData.SongTimeOffset;

        while(_noteAnimationDuration > timeElapsed)
        {
            OnTrackTimeChanged?.Invoke(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _audioSource.Play();

        var timeOffset = _noteAnimationDuration - timeElapsed ;

        while(_track.CurrentTrack.length > _audioSource.time + timeOffset + _noteAnimationDuration)
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
