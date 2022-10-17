using UnityEngine;

[CreateAssetMenu(menuName = "Story/Story level", fileName = "Story Level")]
public class StoryLevel : ScriptableObject
{
    [SerializeField]
    private StoryTrack _track;
    [SerializeField]
    private StoryLevel _nextLevel;

    public StoryTrack Track => _track;
    public StoryLevel NextLevel => _nextLevel;
}