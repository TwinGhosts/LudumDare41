using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameState GameState = GameState.CINEMATIC;
    public static Difficulty Difficulty = Difficulty.NORMAL;

    private static float pMs = 0f;

    private bool _isPaused = false;

    public GameObject pauseCanvas;
    public GameObject loseCanvas;

    public UnityEvent OnWin;

    [SerializeField]
    private AnimationCurve _introCurve;

    // Use this for initialization
    private void Awake()
    {
        GameState = GameState.CINEMATIC;
        Util.GameController = this;
    }

    private IEnumerator Start()
    {
        yield return StartCoroutine(IntroCinematic());
        GameState = GameState.PLAYING;
    }

    public void Lose()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseToggle();
        }
    }

    public void Pause(bool pause)
    {
        if (GameState == GameState.PLAYING || GameState == GameState.PAUSED)
        {
            pauseCanvas.SetActive(pause);

            if (!_isPaused)
            {
                pMs = Util.Player.Stats.movementSpeed;
                GameState = GameState.PAUSED;
                Time.timeScale = 0f;
            }
            else
            {
                Util.Player.Stats.movementSpeed = pMs;
                Time.timeScale = 1f;
                GameState = GameState.PLAYING;
            }

            _isPaused = pause;
        }
    }

    public void PauseToggle()
    {
        if (GameState == GameState.PLAYING || GameState == GameState.PAUSED)
        {
            if (!_isPaused)
            {
                pMs = Util.Player.Stats.movementSpeed;
                GameState = GameState.PAUSED;
                Time.timeScale = 0f;
            }
            else
            {
                Util.Player.Stats.movementSpeed = pMs;
                Time.timeScale = 1f;
                GameState = GameState.PLAYING;
            }

            _isPaused = !_isPaused;
            pauseCanvas.SetActive(_isPaused);
        }
    }

    public IEnumerator IntroCinematic()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(MovePlayer());

        GameState = GameState.PLAYING;
    }

    public IEnumerator MovePlayer()
    {
        var duration = 1f;
        var progress = 0f;
        var startPosition = Util.Player.transform.position;
        var endPosition = Camera.main.transform.position;

        while (progress < 1f)
        {
            Util.Player.transform.position = Vector2.Lerp(startPosition, endPosition, _introCurve.Evaluate(progress));
            progress += Time.deltaTime / duration;
            yield return null;
        }

        yield return new WaitForSeconds(0.225f);
    }
}

public enum GameState
{
    PLAYING,
    PAUSED,
    CINEMATIC,
}
