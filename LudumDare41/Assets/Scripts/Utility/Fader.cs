using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private float _fadeDuration = 0.5f;
    private Image _image;


    // Use this for initialization
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        StartCoroutine(_Fade(true));
    }

    private void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings + 1 < SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("ERROR > Final Level Reached!");
        }
    }

    private void SwitchLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void SwitchLevel(string index)
    {
        SceneManager.LoadScene(index);
    }

    public IEnumerator _Fade(bool fadeIn, SceneManagementTypes? type = null, int? index = null, string sceneName = null)
    {
        var progress = 0f;
        var color = _image.color;
        while (progress < 1f)
        {
            progress += Time.deltaTime / _fadeDuration;

            color.a = (fadeIn) ? 1f - progress : progress;
            _image.color = color;

            yield return null;
        }

        if (!fadeIn && type != null)
        {
            switch (type)
            {
                case SceneManagementTypes.NEXT:
                    NextLevel();
                    break;
                case SceneManagementTypes.SWITCH_INT:
                    SwitchLevel((int)index);
                    break;
                case SceneManagementTypes.SWITCH_STRING:
                    SwitchLevel((string)sceneName);
                    break;
            }
        }
    }
}

public enum SceneManagementTypes
{
    NEXT,
    SWITCH_INT,
    SWITCH_STRING,
}
