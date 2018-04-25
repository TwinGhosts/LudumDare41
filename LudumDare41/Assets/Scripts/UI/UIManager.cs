using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _currentLevel;
    [SerializeField]
    private Slider _healthBar;
    [SerializeField]
    private Slider _manaBar;
    [SerializeField]
    private Slider _experienceBar;

    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private Text _manaText;
    [SerializeField]
    private Text _experienceText;

    private void Awake()
    {
        Util.UIManager = this;
    }

    public void UpdateUIProperties(UnitBase unit)
    {
        _currentLevel.text = unit.Stats.level.ToString();

        _healthBar.maxValue = unit.Stats.maxHealth;
        _healthBar.value = unit.Stats.health;

        _manaBar.maxValue = unit.Stats.maxMana;
        _manaBar.value = unit.Stats.mana;

        _experienceBar.maxValue = unit.Stats.MaxExperience;
        _experienceBar.value = unit.Stats.experience;

        _healthText.text = (int)unit.Stats.health + " / " + (int)unit.Stats.maxHealth;
        _manaText.text = (int)unit.Stats.mana + " / " + (int)unit.Stats.maxMana;
        _experienceText.text = (int)unit.Stats.experience + " / " + (int)unit.Stats.NextLevelExperience;
    }
}
