using System.Collections;
using UnityEditor;
using UnityEngine;

public class LevelStartClock : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeLevelStart = 2;

    public IEnumerator StartClock()
    {
        Debug.Log("Clock start");
        yield return new WaitForSeconds(_timeBeforeLevelStart);
        Debug.Log("Clock end");
    }

}