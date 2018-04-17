using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class TransitionBase : MonoBehaviour
{
    [SerializeField]
    protected bool _transitionIntroAtStart = false;

    [SerializeField]
    protected float _transitionDuration = 0.5f;

    [SerializeField]
    protected AnimationCurve _animationCurve;

    protected void NextLevel()
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

    protected void SwitchLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    protected void SwitchLevel(string index)
    {
        SceneManager.LoadScene(index);
    }

    public virtual IEnumerator _Transition(bool fadeIn, SceneManagementTypes? type = null, int? index = null, string sceneName = null)
    {
        yield return null;
    }

    protected virtual void LevelSwitchType(bool fadeIn, SceneManagementTypes? type, int? index = null, string sceneName = null)
    {
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
                    SwitchLevel(sceneName);
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
