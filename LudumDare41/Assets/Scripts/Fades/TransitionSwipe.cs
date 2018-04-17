using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionSwipe : TransitionBase
{
    [SerializeField]
    private GameObject _image;

    private Vector2 _startPosition, _middlePosition, _endPosition;
    private Vector2 _nextPosition, _currentPosition;

    // Use this for initialization
    private void Awake()
    {
        _startPosition = new Vector2(-1920f, 0f);
        _middlePosition = new Vector2(0f, 0f);
        _endPosition = new Vector2(1920f, 0f);

        _currentPosition = _startPosition;
        _nextPosition = _middlePosition;

        _image.GetComponent<RectTransform>().anchoredPosition = _currentPosition;

        if (_transitionIntroAtStart)
        {
            StartCoroutine(_Transition(true, TransitionSwipeEndPosition.MIDDLE_END));
            _nextPosition = _endPosition;
        }
    }

    public IEnumerator _Transition(bool fadeIn, TransitionSwipeEndPosition endPosType, SceneManagementTypes? type = null, int? index = null, string sceneName = null)
    {
        SetPosBasedOnEnum(endPosType);

        var progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / _transitionDuration;
            _image.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(_currentPosition, _nextPosition, _animationCurve.Evaluate(progress));
            yield return null;
        }

        LevelSwitchType(fadeIn, type, index, sceneName);
    }

    private void SetPosBasedOnEnum(TransitionSwipeEndPosition endPosType)
    {
        switch (endPosType)
        {
            case TransitionSwipeEndPosition.MIDDLE_START:
                _currentPosition = _middlePosition;
                _nextPosition = _startPosition;
                break;
            case TransitionSwipeEndPosition.START_MIDDLE:
                _currentPosition = _startPosition;
                _nextPosition = _middlePosition;
                break;
            case TransitionSwipeEndPosition.END_MIDDLE:
                _currentPosition = _endPosition;
                _nextPosition = _middlePosition;
                break;
            case TransitionSwipeEndPosition.MIDDLE_END:
                _currentPosition = _middlePosition;
                _nextPosition = _endPosition;
                break;
        }
    }

    public enum TransitionSwipeEndPosition
    {
        MIDDLE_START,
        START_MIDDLE,
        END_MIDDLE,
        MIDDLE_END,
    }
}
