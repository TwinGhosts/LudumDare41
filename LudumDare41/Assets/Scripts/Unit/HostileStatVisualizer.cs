using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitBase))]
public class HostileStatVisualizer : MonoBehaviour
{
    public Slider healthbar;
    public bool hideOnFullHealth = true;

    private UnitBase _unit;

    private void Awake()
    {
        _unit = GetComponent<UnitBase>();
        healthbar.maxValue = _unit.Stats.maxHealth;
        healthbar.value = _unit.Stats.health;
        ShowHealthbar();
    }

    // Update is called once per frame
    private void Update()
    {
        healthbar.maxValue = _unit.Stats.maxHealth;
        healthbar.value = _unit.Stats.health;
        ShowHealthbar();
    }

    private void ShowHealthbar()
    {
        if (hideOnFullHealth && _unit.Stats.health >= _unit.Stats.maxHealth)
        {
            healthbar.gameObject.SetActive(false);
        }
        else if (!hideOnFullHealth || (hideOnFullHealth && _unit.Stats.health < _unit.Stats.maxHealth))
        {
            healthbar.gameObject.SetActive(true);
        }
    }
}
