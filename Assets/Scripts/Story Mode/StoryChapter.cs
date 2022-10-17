using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/Story chapter" , fileName = "Story Chapter")]
public class StoryChapter : ScriptableObject
{
    [SerializeField]
    private Sprite _cover;
    [SerializeField]
    private StoryLevel _firstLevel;
    [SerializeField]
    private StoryLevel _lastLevel;
    [SerializeField]
    private StoryChapter _nextChapter;
    [SerializeField]
    private int _chapterNumber;
    [SerializeField]
    private LevelType _levelType;

    public Sprite Cover => _cover;
    public StoryLevel FirstLevel => _firstLevel;
    public StoryLevel LastLevel => _lastLevel;  
    public StoryChapter NextChapter => _nextChapter;
    public int ChapterNumber => _chapterNumber;
    public LevelType LevelType => _levelType;
}