using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileBehaviour : MonoBehaviour
{
    [SerializeField]
    protected List<ProjectileBase> _projectileTypeList = new List<ProjectileBase>();

    protected abstract IEnumerator Shoot();
    protected abstract IEnumerator Shoot(Transform target);
    protected abstract IEnumerator Shoot(Vector2 location);
    protected abstract IEnumerator Chase(Transform target);
    protected abstract IEnumerator Move(Vector2 location);
}
