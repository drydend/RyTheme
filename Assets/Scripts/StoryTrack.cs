using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Story track", menuName = "Story track")]
public class StoryTrack : ScriptableObject
{
    [SerializeField]
    private string _smFileName;
    public string SMFileName => _smFileName;
}
