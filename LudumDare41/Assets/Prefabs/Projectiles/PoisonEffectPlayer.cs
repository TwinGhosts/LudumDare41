using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffectPlayer : MonoBehaviour
{
    public float duration = 5f;
    private float currentTime = 0f;
    public float damagePerTick = 1f;
    public int nrOfTicks = 5;

    private int tickIndex = 0;

    public void Update()
    {
        if (currentTime > duration)
        {
            Destroy(this);
        }

        if (currentTime > (duration / nrOfTicks) * tickIndex)
        {
            Damage();
            tickIndex++;
        }
    }

    private void Damage()
    {
        if (GetComponent<Player>())
        {
            GetComponent<Player>().Damage(damagePerTick);
        }
        else if (GetComponent<UnitBase>())
        {
            GetComponent<UnitBase>().Stats.Damage(damagePerTick);
        }
    }
}
