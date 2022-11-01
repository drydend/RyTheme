using TMPro;
using UnityEngine;
using Zenject;

public class LevelScoreUI : MonoBehaviour
{
    [SerializeField]
    private int _minNumberOfLetters = 7;
    [SerializeField]
    private TMP_Text _text;
    private LevelScore _levelScore;

    [Inject]
    public void Construct(LevelScore levelScore)
    {
        _levelScore = levelScore;
    }

    private void OnEnable()
    {
        _levelScore.OnScoreChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        _levelScore.OnScoreChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        _text.text = _levelScore.CurrentScore.ToStringWithZeros(_minNumberOfLetters);
    }
}
