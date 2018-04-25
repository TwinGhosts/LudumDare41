using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionFade : TransitionBase
{
    [SerializeField]
    private Image _image;

    // Use this for initialization
    private void Awake()
    {
        if (_transitionIntroAtStart)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            StartCoroutine(_Transition(true));
        }
    }

    public override IEnumerator _Transition(bool fadeIn, SceneManagementTypes? type = null, int? index = null, string sceneName = null)
    {
        Time.timeScale = 1f;
        var progress = 0f;
        var color = _image.color;
        while (progress < 1f)
        {
            progress += Time.deltaTime / _transitionDuration;

            color.a = (fadeIn) ? _animationCurve.Evaluate(1f - progress) : _animationCurve.Evaluate(progress);
            _image.color = color;

            yield return null;
        }

        LevelSwitchType(fadeIn, type, index, sceneName);
    }
}