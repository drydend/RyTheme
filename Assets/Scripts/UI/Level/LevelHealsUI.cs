using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelHealsUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private LevelHeals _levelHeals;

    [Inject]
    public void Construct(LevelHeals levelHeals)
    {
        _levelHeals = levelHeals;
    }

    private void OnEnable()
    {
        _levelHeals.OnHealsChanged += UpdateValue;
        UpdateValue();
    }

    private void OnDisable()
    {
        _levelHeals.OnHealsChanged -= UpdateValue;
    }

    private void UpdateValue()
    {
        _slider.value = _levelHeals.CurrentHeals / _levelHeals.MaxHeals;
    }
}
