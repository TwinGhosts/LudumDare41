using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _railList;

    [SerializeField]
    private float _railSpawnInterval = 8f;
    private float _railSpawnIntervalOffset = 3f;
    private bool _isSpawning = false;

    // Use this for initialization
    private void Awake()
    {
        Util.StageGenerator = this;
    }

    private void Update()
    {
        if (!_isSpawning)
        {
            StartCoroutine(_SpawnRail((int)Random.Range(0f, 1f)));
        }
    }

    private IEnumerator _SpawnRail(int value)
    {
        _isSpawning = true;

        yield return new WaitForSeconds(_railSpawnInterval + Random.Range(-_railSpawnIntervalOffset, _railSpawnIntervalOffset));

        var trackStartingPosition = Camera.main.transform.position + (Vector3)new Vector2(Random.Range(-5f, 5f), 10f + Random.Range(-5f, 5f));
        trackStartingPosition.z = 0f;
        var rail = Instantiate(_railList[Random.Range(0, _railList.Length)]);
        rail.transform.position = trackStartingPosition;

        _isSpawning = false;
    }
}
