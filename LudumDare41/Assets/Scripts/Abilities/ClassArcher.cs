using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassArcher : ClassBase
{
    private bool _hasSpeed = false;

    protected void Start()
    {
        SetStats();
    }

    protected override void Update()
    {
        base.Update();

        if (_abilityCooldowns.Count != 0)
        {
            _abilityCooldownsMax[0] = 0.5f - (GetComponent<UnitBase>().Stats.level * 0.04f);
        }
    }

    // Normal arrow shot which gets faster by the level
    protected override void AbilityOne()
    {
        var stats = GetComponent<UnitBase>().Stats;

        var arrow = Instantiate(GetComponent<Player>()._archerAbOneProjectile);
        arrow.transform.position = transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var finalAngle = Util.AngleFromVector(new Vector2(mousePos.x, mousePos.y));

        arrow.GoToMouse(true);
        arrow.SetAngle(finalAngle);
        arrow.Damage = stats.attackPower;
        arrow.Speed = 8.5f + stats.level * 0.125f;
    }

    // 360 poison arrow shot, speed and arrowCount increase with levels
    protected override void AbilityTwo()
    {
        var stats = GetComponent<UnitBase>().Stats;
        var angle = 0f;
        var arrowCount = 5 + (int)(0.25f * stats.level);

        for (int i = 0; i < arrowCount; i++)
        {
            var arrow = Instantiate(GetComponent<Player>()._archerAbTwoProjectile);
            var finalAngle = angle + i * (360f / arrowCount);

            arrow.transform.position = transform.position;
            arrow.SetAngle(finalAngle);
            arrow.Damage = stats.attackPower * (1.5f + stats.level * 0.033f);
            arrow.Speed = 10f;
        }
    }

    // Single lighting arrow which chains to nearby enemies
    protected override void AbilityThree()
    {
        var stats = GetComponent<UnitBase>().Stats;

        var arrow = Instantiate(GetComponent<Player>()._archerAbThreeProjectile);
        arrow.transform.position = transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        arrow.SetDirection(new Vector2(mousePos.x, mousePos.y));
        arrow.GoToMouse(true);
        arrow.Damage = stats.attackPower;
        arrow.Speed = 6 + stats.level * 0.15f;
    }

    // Flat movementSpeed increase for a couple of seconds based on level
    public override void AbilityFour()
    {
        var stats = GetComponent<UnitBase>().Stats;
        if (!_hasSpeed)
        {
            StartCoroutine(IncreasedMovementSpeed(stats));
        }
    }

    private IEnumerator IncreasedMovementSpeed(UnitStats stats)
    {
        _hasSpeed = true;
        var currentSpeed = stats.maxMovementSpeed;
        var duration = 1.5f + stats.level * 0.15f;

        stats.maxMovementSpeed = currentSpeed * 1.66f;

        yield return new WaitForSeconds(duration);

        stats.maxMovementSpeed = currentSpeed;

        _hasSpeed = false;
    }

    protected override void SetStats()
    {
        _abilityCooldowns.Add(0f);
        _abilityCooldowns.Add(0f);
        _abilityCooldowns.Add(0f);
        _abilityCooldowns.Add(0f);

        _abilityCooldownsMax.Add(0.5f);
        _abilityCooldownsMax.Add(8f);
        _abilityCooldownsMax.Add(8f);
        _abilityCooldownsMax.Add(10f);

        _manaCost.Add(0f);
        _manaCost.Add(10f);
        _manaCost.Add(6f);
        _manaCost.Add(8f);

        GetComponent<UnitBase>().Stats.health = 40f;
        GetComponent<UnitBase>().Stats.maxHealth = 40f;
        GetComponent<UnitBase>().Stats.attackPower = 4f;
        GetComponent<UnitBase>().Stats.mana = 50f;
        GetComponent<UnitBase>().Stats.healthRegen = 0.2f;
        GetComponent<UnitBase>().Stats.manaRegen = 1f;
        GetComponent<UnitBase>().Stats.criticalHitChance = 5f;

        GetComponent<UnitBase>().Stats.levelUpAttackPower = 1f;
        GetComponent<UnitBase>().Stats.levelUpHealth = 3f;
        GetComponent<UnitBase>().Stats.levelUpMana = 1f;
        GetComponent<UnitBase>().Stats.levelUpHealthRegen = 0.1f;
        GetComponent<UnitBase>().Stats.levelUpManaRegen = 0.25f;
        GetComponent<UnitBase>().Stats.levelUpCriticalHitChance = 1f;

        Util.UIManager.UpdateUIProperties(Util.Player);
    }
}
