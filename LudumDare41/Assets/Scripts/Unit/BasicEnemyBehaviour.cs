using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehaviour : HostileBehaviour
{
    public float projectileSpeed = 5f;
    public int projectileCount = 5;
    public float targetAngle = 0f;
    public float shootInterval = 3f;

    private bool _isShooting = false;

    public bool stayNearPlayer = true;
    public float stayNearFraction = 0.9f;

    private void Awake()
    {
        switch (GameController.Difficulty)
        {
            default:
            case Difficulty.EASY:
                projectileCount = 4;
                break;
            case Difficulty.NORMAL:
                projectileCount = 6;
                break;
            case Difficulty.HARD:
                projectileCount = 8;
                break;
        }
    }

    private void Update()
    {
        if (stayNearPlayer)
        {
            transform.position += (Vector3)new Vector2(0f, Util.Player.Stats.movementSpeed * stayNearFraction * Time.deltaTime);
        }

        if (!_isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    protected override IEnumerator Shoot()
    {
        _isShooting = true;

        yield return new WaitForSeconds(shootInterval);

        var standardAngle = Random.Range(0f, 360f);

        for (int i = 0; i < projectileCount; i++)
        {
            var projectile = Instantiate(_projectileTypeList[0]);

            var offset = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
            var finalAngle = standardAngle + i * (360f / projectileCount);
            var offsetVector = Util.VectorFromAngle(finalAngle).normalized;

            projectile.transform.position = transform.position + ((Vector3)offsetVector * offset);

            projectile.GetComponent<ProjectileBase>().Speed = projectileSpeed;
            projectile.GetComponent<ProjectileBase>().LifeTimer = 20f;
            projectile.GetComponent<ProjectileBase>().Damage = GetComponent<UnitBase>().Stats.attackPower;

            if (projectile.GetComponent<AngledProjectile>())
            {
                projectile.GetComponent<AngledProjectile>().SetAngle(finalAngle);
            }
        }

        _isShooting = false;
    }

    #region Override stuff
    protected override IEnumerator Shoot(Transform target)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Shoot(Vector2 location)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Chase(Transform target)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Move(Vector2 location)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
