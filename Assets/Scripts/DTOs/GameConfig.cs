using UnityEngine;

[CreateAssetMenu(menuName = "Game config" , fileName = "Game config")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private float _noteTimeDeltaForPerfect = 0.05f;
    [SerializeField]
    private float _noteTimeDeltaForGood = 0.1f;
    [SerializeField]
    private float _noteTimeDeltaForOk = 0.15f;

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
    
    public float NoteTimeDeltaForPerfect => _noteTimeDeltaForPerfect;
    public float NoteTimeDeltaForGood => _noteTimeDeltaForGood; 
    public float NoteTimeDeltaForOk => _noteTimeDeltaForOk;

    public int ScoreForOk => _scoreForOk;
    public int ScoreForGood => _scoreForGood;
    public int ScoreForPerfect => _scoreForPerfect;

    public float NoteTimeToCrossing => _noteTimeToCrossing;
    public float DamageForMiss => _damageForMiss;
}
