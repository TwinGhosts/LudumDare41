using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameState GameState = GameState.PLAYING;

    private bool _isPaused = false;
    private float _previousTimeScale = 1f;
    private float _standardTimeScale = 1f;

    // Use this for initialization
    private void Start()
    {
        Util.GameController = this;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void PauseToggle()
    {
        if (!_isPaused)
        {
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = _previousTimeScale;
        }

        _isPaused = !_isPaused;
    }

    private void GameTimeChange(bool forward)
    {
        _previousTimeScale = Time.timeScale;

        Time.timeScale = (forward) ? Time.timeScale++ : Time.timeScale--;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 5f);
    }
}

public enum GameState
{
    PLAYING,
    PAUSED,
    CINEMATIC,
    WIN,
    LOSE,
}
