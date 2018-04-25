using UnityEngine;

[System.Serializable]
public class UnitStats
{
    [Header("Base Stats")]
    public float health = 50;
    public float maxHealth = 50;

    public float mana = 10;
    public float maxMana = 10;

    [Header("Type")]
    public UnitType type = UnitType.NORMAL;

    [Header("Experience")]
    [SerializeField]
    private AnimationCurve experienceCurve;

    public float level = 1;
    public float experience = 0;
    private float _maxLevel = 25;
    public float experienceOnDeath = 10f;

    [Header("Movement")]
    [HideInInspector]
    public float movementSpeed = 0f;
    public float maxMovementSpeed = 5f;

    [Header("Power")]
    public float attackPower = 2.5f;
    public float abilityPower = 0f;

    [Header("Defense")]
    public float physicalDefense = 5f;
    public float magicDefense = 5f;

    [Header("Regen")]
    public float healthRegen = 0.01f;
    public float manaRegen = 0.25f;

    [Header("Chances")]
    public float criticalHitChance = 0f;
    public float dodgeChance = 0f;
    public float parryChance = 0f;
    public float blockChance = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 16f;
    public float maxRotationSpeed = 16f;

    [Header("Stat Gain Per LevelUp")]
    public float levelUpHealth = 0f;
    public float levelUpMana = 0f;
    public float levelUpAttackPower = 0f;
    public float levelUpAbilityPower = 0f;
    public float levelUpPhysicalDefense = 0f;
    public float levelUpMagicDefense = 0f;
    public float levelUpHealthRegen = 0.25f;
    public float levelUpManaRegen = 0.2f;
    public float levelUpCriticalHitChance = 0f;
    public float levelUpDodgeChance = 0f;
    public float levelUpParryChance = 0f;
    public float levelUpBlockChance = 0f;

    private const float EXPERIENCE_MULTIPLIER = 600f;

    public void Init()
    {
        NextLevelExperience = experienceCurve.Evaluate((level + 1) / _maxLevel) * EXPERIENCE_MULTIPLIER;
    }

    public void Damage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0f, 10000f);
    }

    public void RegenerateStats()
    {
        AddHealth(healthRegen / 4f);
        AddMana(manaRegen / 4f);
    }

    public void AddHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0f, maxHealth);
    }

    public void AddMana(float amount)
    {
        mana = Mathf.Clamp(mana + amount, 0f, maxMana);
    }

    public void AddExperience(float amount)
    {
        if (level < _maxLevel)
        {
            if (experience + amount >= NextLevelExperience)
            {
                var tempExperience = experience + amount - NextLevelExperience;
                experience = tempExperience;
                LevelUpStats();
                level++;
                NextLevelExperience = experienceCurve.Evaluate((level + 1) / _maxLevel) * EXPERIENCE_MULTIPLIER;
            }
            else
            {
                experience += amount;
            }
        }
        else
        {
            experience = NextLevelExperience;
        }
    }

    private void LevelUpStats()
    {
        maxHealth += levelUpHealth;
        health += levelUpHealth;
        maxMana += levelUpMana;
        mana += levelUpMana;
        attackPower += levelUpAttackPower;
        abilityPower += levelUpAbilityPower;
        physicalDefense += levelUpPhysicalDefense;
        magicDefense += levelUpMagicDefense;
        healthRegen += levelUpHealthRegen;
        manaRegen += levelUpManaRegen;
        criticalHitChance += levelUpCriticalHitChance;
        dodgeChance += levelUpDodgeChance;
        parryChance += levelUpParryChance;
        blockChance += levelUpBlockChance;
    }

    public float MaxExperience
    {
        get
        {
            return NextLevelExperience;
        }
    }

    public bool IsFullHealth
    {
        get
        {
            return health >= maxHealth;
        }
    }

    public bool IsFullMana
    {
        get
        {
            return mana >= maxMana;
        }
    }

    public float NextLevelExperience
    {
        get; set;
    }
}

public enum UnitType
{
    NONE,
    NORMAL,
    CONSTRUCTION,
    MINI_BOSS,
    BOSS,
}
