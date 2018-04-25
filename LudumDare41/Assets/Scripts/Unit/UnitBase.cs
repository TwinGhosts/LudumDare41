using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class UnitBase : MonoBehaviour
{
    [SerializeField]
    protected UnitStats _stats = new UnitStats();
    protected UnitMovement _movement;
    protected bool _isRegenerating = false;

    protected const float REGENERATION_INTERVAL = 0.25f;

    public UnitStats Stats
    {
        get
        {
            return _stats;
        }
    }

    // Use this for initialization
    protected virtual void Awake()
    {
        _stats.Init();
        _movement = GetComponent<UnitMovement>();
        _movement.SetBase(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GetComponent<Player>() && Stats.health <= 0f)
        {
            Util.Player.Stats.AddExperience(Stats.experienceOnDeath);
            Destroy(gameObject);
        }

        if (transform.position.y > Camera.main.transform.position.y + 10f)
        {
            Destroy(gameObject);
        }

        if (!_isRegenerating && GameController.GameState == GameState.PLAYING)
        {
            StartCoroutine(RegenerateStats());
        }
    }

    protected IEnumerator RegenerateStats()
    {
        _isRegenerating = true;
        yield return new WaitForSeconds(REGENERATION_INTERVAL);
        _stats.RegenerateStats();
        _isRegenerating = false;
    }
}
