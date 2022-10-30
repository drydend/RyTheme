using Mono.Cecil.Cil;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class LevelStartClockUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private AnimationCurve _curve;
    [SerializeField]
    [Range(0, 1)]
    private float _animationDuration = 1;

    private int _lastSecond;

    private LevelStartClock _startClock;

    [Inject]
    public void Construct(LevelStartClock startClock)
    {
        _startClock = startClock;
    }

    private void OnEnable()
    {
        _startClock.OnStarted += UpdateUI;
    }

    private void OnDisable()
    {
        _startClock.OnStarted -= UpdateUI;
    }

    private void UpdateUI()
    {
        StartCoroutine(ShowAnimation());
    }

    private IEnumerator ShowAnimation()
    {
        _text.gameObject.SetActive(true);

        var second = _startClock.TimeBeforeStart - 1;

        while (second >= 0)
        {
            _text.text = second.ToString();
            _text.gameObject.SetActive(true);
            _text.rectTransform.localScale = Vector3.one;

            var timeElapsed = 0f;
            while (timeElapsed < _animationDuration)
            {
                var currentValue = _curve.Evaluate(timeElapsed);
                _text.rectTransform.localScale = new Vector3(currentValue, currentValue, currentValue);
                _text.text = second.ToString();
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _text.gameObject.SetActive(false);
            yield return new WaitForSeconds(1 - timeElapsed);
            second--;

        }

        _text.gameObject.SetActive(false);
    }
}
