using UnityEngine;

[CreateAssetMenu(menuName = "Game config" , fileName = "Game config")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private float _noteTimeToCrossing = 2;
    [SerializeField]
    private float _noteTimeDeltaForPerfect = 0.05f;
    [SerializeField]
    private float _noteTimeDeltaForGood = 0.1f;
    [SerializeField]
    private float _noteTimeDeltaForOk = 0.15f;
    public float NoteTimeToCrossing => _noteTimeToCrossing;
    public float NoteTimeDeltaForPerfect => _noteTimeDeltaForPerfect;
    public float NoteTimeDeltaForGood => _noteTimeDeltaForGood; 
    public float NoteTimeDeltaForOk => _noteTimeDeltaForOk; 

}
