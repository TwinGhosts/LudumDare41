using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected string _name = "Generic Ability";

    [SerializeField]
    protected float _cooldown = 5f;

    [SerializeField]
    protected float _castTime = 0f;

    public abstract void Activate();
}
