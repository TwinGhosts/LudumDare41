using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public SpawnWrapper[] _spawnInfo;
    public float[] _spawnTimes;

    public GameObject _boss;
    private float _currentTime = 0f;
    private int timerIndex = 0;
    private bool _isWinning = false;

    // Update is called once per frame
    private void Update()
    {
        if (GameController.GameState == GameState.PLAYING)
        {
            _currentTime += Time.deltaTime;
        }

        if (timerIndex < _spawnTimes.Length && _spawnTimes[timerIndex] < _currentTime)
        {
            SpawnWave(_spawnInfo[timerIndex]);
            timerIndex++;
        }
        else if (timerIndex == _spawnTimes.Length && FindObjectsOfType<Hostile>().Length == 0)
        {
            if (!_isWinning)
            {
                StartCoroutine(Win());
            }
        }
    }

    private IEnumerator Win()
    {
        _isWinning = true;
        yield return new WaitForSeconds(2f);
        Util.GameController.OnWin.Invoke();
    }

    private void SpawnWave(SpawnWrapper info)
    {
        var index = 0;
        foreach (var unit in info.spawnUnits)
        {
            var o = Instantiate(info.spawnUnits[index], Util.WorldSpaceCanvas);
            o.transform.position = new Vector2(0f + info.spawnLocations[index].x, Camera.main.transform.position.y + info.spawnLocations[index].y);

            index++;
        }
    }
}

[System.Serializable]
public class SpawnWrapper
{
    public Vector2[] spawnLocations;
    public GameObject[] spawnUnits;

    public SpawnWrapper()
    {
    }
}
