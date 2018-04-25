using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassBase : MonoBehaviour
{
    [SerializeField]
    protected List<float> _abilityCooldowns = new List<float>();
    protected List<float> _abilityCooldownsMax = new List<float>();
    [SerializeField]
    protected List<float> _manaCost = new List<float>();

    protected virtual void Update()
    {
        var index = 0;
        foreach (var cd in _abilityCooldowns)
        {
            if (cd > 0f)
            {
                _abilityCooldowns[index] -= Time.deltaTime;
            }
            index++;
        }

        var stats = GetComponent<UnitBase>().Stats;
        if (Input.GetMouseButton(0) && stats.mana >= _manaCost[0] && _abilityCooldowns[0] <= 0f)
        {
            ActivateAbility(1);
            GetComponent<UnitBase>().Stats.mana -= _manaCost[0];
        }

        if (Input.GetMouseButton(1) && stats.mana >= _manaCost[1] && _abilityCooldowns[1] <= 0f)
        {
            ActivateAbility(2);
            GetComponent<UnitBase>().Stats.mana -= _manaCost[1];
        }

        if (Input.GetKeyDown(KeyCode.Q) && stats.mana >= _manaCost[2] && _abilityCooldowns[2] <= 0f)
        {
            ActivateAbility(3);
            GetComponent<UnitBase>().Stats.mana -= _manaCost[2];
        }

        if (Input.GetKeyDown(KeyCode.E) && stats.mana >= _manaCost[3] && _abilityCooldowns[3] <= 0f)
        {
            ActivateAbility(4);
            GetComponent<UnitBase>().Stats.mana -= _manaCost[3];
        }
    }

    public virtual void ActivateAbility(int index)
    {
        switch (index)
        {
            default:
            case 1:
                if (_abilityCooldowns[0] <= 0f)
                {
                    AbilityOne();
                    _abilityCooldowns[0] = _abilityCooldownsMax[0];
                }
                break;
            case 2:
                if (_abilityCooldowns[1] <= 0f)
                {
                    AbilityTwo();
                    _abilityCooldowns[1] = _abilityCooldownsMax[1];
                }
                break;
            case 3:
                if (_abilityCooldowns[2] <= 0f)
                {
                    AbilityThree();
                    _abilityCooldowns[2] = _abilityCooldownsMax[2];
                }
                break;
            case 4:
                if (_abilityCooldowns[3] <= 0f)
                {
                    AbilityFour();
                    _abilityCooldowns[3] = _abilityCooldownsMax[3];
                }
                break;
        }
    }

    protected virtual void AbilityOne()
    {

    }

    protected virtual void AbilityTwo()
    {

    }

    protected virtual void AbilityThree()
    {

    }

    public virtual void AbilityFour()
    {

    }

    protected abstract void SetStats();
}
