using TMPro;
using UnityEngine;
using Zenject;

public class PlaylistLevelWinScrene : WinScrene
{
    private LevelScore _levelScore;
    private LevelDataProvider _levelDataProvider;

    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _artist;
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private int _minNumberOfLetters = 7;

    [Inject]
    public void Construct(LevelScore levelScore, LevelDataProvider levelDataProvider)
    {
        _levelScore = levelScore;
        _levelDataProvider = levelDataProvider;
    }

    public override void Show()
    {   
        base.Show();
        _title.text = _levelDataProvider.CurrentLevelData.Title;
        _artist.text = _levelDataProvider.CurrentLevelData.Artist;
        _scoreText.text = _levelScore.CurrentScore.ToStringWithZeros(_minNumberOfLetters);
    }
}
