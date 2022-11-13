using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ChainMenuItem : MonoBehaviour
{
    [SerializeField]
    private float _selectedScale;
    [SerializeField]
    private float _defaultScale;
    [SerializeField]
    private float _lerpFactor = 0.8f;
    [SerializeField]
    private float _animationDurationFactor = 1;

    private Coroutine _selectedCoroutine;
    private Coroutine _unselectedCoroutine;

    private RectTransform _rectTransform;

    private bool _isSelected;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            return _rectTransform;
        }
    }

    public virtual void OnUnselected()
    {
        if (_selectedCoroutine != null)
        {
            StopCoroutine(_selectedCoroutine);
        }

        if (_unselectedCoroutine != null)
        {
            StopCoroutine(_unselectedCoroutine);
        }

        _isSelected = false;
        _selectedCoroutine = StartCoroutine(PlayUnselectedAnimation());
    }

    public virtual void OnSelected()
    {
        if (_selectedCoroutine != null)
        {
            StopCoroutine(_selectedCoroutine);
        }

        if (_unselectedCoroutine != null)
        {
            StopCoroutine(_unselectedCoroutine);
        }

        _isSelected = true;
        _unselectedCoroutine = StartCoroutine(PlaySelectedAnimation());
    }

    private void OnEnable()
    {
        if (_isSelected)
        {
            RectTransform.localScale = new Vector3(_selectedScale, _selectedScale, _selectedScale);
        }
        else
        {
            RectTransform.localScale = new Vector3(_defaultScale, _defaultScale, _defaultScale);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator PlaySelectedAnimation()
    {
        var currentScale = RectTransform.localScale.x;
        while (currentScale != _selectedScale)
        {
            currentScale = Mathf.Lerp(currentScale, _selectedScale,
                _lerpFactor * Time.deltaTime * _animationDurationFactor);

            RectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }
    }

    private IEnumerator PlayUnselectedAnimation()
    {
        var currentScale = RectTransform.localScale.x;
        while (currentScale != _defaultScale)
        {
            currentScale = Mathf.Lerp(currentScale, _defaultScale,
                _lerpFactor * Time.deltaTime * _animationDurationFactor);

            RectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }
    }
}