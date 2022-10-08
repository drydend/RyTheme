using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Track", menuName = "Track")]
public class Track : ScriptableObject
{
    [SerializeField]
    private string _smFileName;
    [SerializeField]
    private AudioClip _track;
    public AudioClip CurrentTrack => _track;
    public string SMFileName => _smFileName;
}
