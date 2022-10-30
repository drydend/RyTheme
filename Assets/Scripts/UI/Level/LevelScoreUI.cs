using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        var stringNumber = _levelScore.CurrentScore.ToString();

        if(stringNumber.Length < _minNumberOfLetters)
        {
            string zerosSequence = "";

            for(int i = 0; i < _minNumberOfLetters - stringNumber.Length; i++)
            {
                zerosSequence += "0";
            }

            stringNumber = zerosSequence + stringNumber;
        }

        _text.text = stringNumber;
    }
}
