using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChainMenuStoryChapterItem : ChainMenuItem
{
    private StoryChapter _chapter;

    private Image _image;
    public StoryChapter Chapter => _chapter;

    public void Initialize(StoryChapter chapter)
    {
        _chapter = chapter;

        _image = GetComponent<Image>();

        _image.sprite = chapter.Cover;
    }
}
