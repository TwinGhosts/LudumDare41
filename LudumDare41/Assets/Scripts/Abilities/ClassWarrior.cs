using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassWarrior : ClassBase
{
    [SerializeField]
    private AngledProjectile _abOneProjectile;
    [SerializeField]
    private AngledProjectile _abTwoProjectile;

    private bool _hasSpeed = false;

    protected void Start()
    {
        SetStats();
    }

    protected override void Update()
    {
        base.Update();
        _abilityCooldownsMax[0] = 0.5f - (GetComponent<UnitBase>().Stats.level * 0.04f);
    }

    // Normal melee attack
    protected override void AbilityOne()
    {
        var stats = GetComponent<UnitBase>().Stats;
    }

    // Blocks all damage for 1.5 seconds
    protected override void AbilityTwo()
    {
        var stats = GetComponent<UnitBase>().Stats;

        GetComponent<Player>().isInvunerable = true;

        var invunerableTime = 1.5f + 0.075f * stats.level;
        StartCoroutine(EndInvunerable(invunerableTime));
    }

    private IEnumerator EndInvunerable(float amount)
    {
        yield return new WaitForSeconds(amount);
        GetComponent<Player>().isInvunerable = false;
    }

    // Shouts a wave which damages enemies in a straight line VECTOR.UP
    protected override void AbilityThree()
    {
        var stats = GetComponent<UnitBase>().Stats;
    }

    // A whirl around the character
    public override void AbilityFour()
    {
        var stats = GetComponent<UnitBase>().Stats;
    }

    protected override void SetStats()
    {
        _abilityCooldowns.Add(0f);
        _abilityCooldowns.Add(0f);
        _abilityCooldowns.Add(0f);
        _abilityCooldowns.Add(0f);

        _abilityCooldownsMax.Add(0.4f);
        _abilityCooldownsMax.Add(7f);
        _abilityCooldownsMax.Add(5f);
        _abilityCooldownsMax.Add(10f);

        _manaCost.Add(0f);
        _manaCost.Add(8f);
        _manaCost.Add(5f);
        _manaCost.Add(5f);

        GetComponent<UnitBase>().Stats.health = 90f;
        GetComponent<UnitBase>().Stats.maxHealth = 90f;
        GetComponent<UnitBase>().Stats.attackPower = 10f;
        GetComponent<UnitBase>().Stats.mana = 25f;
        GetComponent<UnitBase>().Stats.healthRegen = 1f;
        GetComponent<UnitBase>().Stats.manaRegen = 0.5f;
        GetComponent<UnitBase>().Stats.criticalHitChance = 2.5f;

        GetComponent<UnitBase>().Stats.levelUpAttackPower = 2f;
        GetComponent<UnitBase>().Stats.levelUpHealth = 5f;
        GetComponent<UnitBase>().Stats.levelUpMana = 2f;
        GetComponent<UnitBase>().Stats.levelUpHealthRegen = 0.25f;
        GetComponent<UnitBase>().Stats.levelUpManaRegen = 0.2f;
        GetComponent<UnitBase>().Stats.levelUpCriticalHitChance = 1f;
    }
}
