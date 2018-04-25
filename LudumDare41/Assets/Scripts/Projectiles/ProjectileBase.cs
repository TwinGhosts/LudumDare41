using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    protected float _damage = 0f;
    protected float _speed = 0f;
    protected float _lifeTimer = 25f;
    protected bool _touchDamage = true;

    protected UnityEvent OnSpawn;
    protected UnityEvent OnLifeDepleted;
    protected UnityEvent OnDeath;

    public bool hitPlayer = true;

    // Use this for initialization
    protected virtual void Start()
    {
        Destroy(gameObject, LifeTimer);
    }

    protected virtual void Update()
    {
        if (Time.frameCount % 2 == 0)
        {
            if (hitPlayer && Vector2.Distance(transform.position, Util.Player.transform.position) <= Util.Player.GetComponent<CircleCollider2D>().radius)
            {
                Util.Player.Damage(Damage);
                Destroy(gameObject);
            }
        }
    }

    public virtual void Delete()
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    public virtual float Speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    public virtual float Damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
        }
    }

    public virtual float LifeTimer
    {
        get
        {
            return _lifeTimer;
        }

        set
        {
            _lifeTimer = value;
        }
    }
}
