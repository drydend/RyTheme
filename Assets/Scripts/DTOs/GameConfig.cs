using UnityEngine;

[CreateAssetMenu(menuName = "Game config" , fileName = "Game config")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private float _noteDistanceDeltaForPerfect = 0.05f;
    [SerializeField]
    private float _noteDistanceDeltaForGood = 0.1f;
    [SerializeField]
    private float _noteDistanceDeltaForOk = 0.15f;

    [SerializeField]
    private int _scoreForOk = 100;
    [SerializeField]
    private int _scoreForGood = 200;
    [SerializeField]
    private int _scoreForPerfect = 300;

    [SerializeField]
    private float _noteTimeToCrossing = 2;
    [SerializeField]
    private float _damageForMiss;
    
    public float NoteDistanceDeltaForPerfect => _noteDistanceDeltaForPerfect;
    public float NoteDistanceDeltaForGood => _noteDistanceDeltaForGood; 
    public float NoteDistanceDeltaForOk => _noteDistanceDeltaForOk;

    public int ScoreForOk => _scoreForOk;
    public int ScoreForGood => _scoreForGood;
    public int ScoreForPerfect => _scoreForPerfect;

    public float NoteTimeToCrossing => _noteTimeToCrossing;
    public float DamageForMiss => _damageForMiss;
}
