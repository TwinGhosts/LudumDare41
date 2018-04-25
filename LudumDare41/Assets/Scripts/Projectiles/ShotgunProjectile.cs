using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : MonoBehaviour
{
    public float projectileSpeed = 7f;
    public int projectileCount = 3;
    public float targetAngle = 0f;
    public float shootInterval = 3f;
    public float totalSpreadAngle = 120f;

    public GameObject prefab;

    public bool shootAtPlayer = true;

    private bool _isShooting = false;

    public bool stayNearPlayer = true;
    public float stayNearFraction = 0.7f;

    // Use this for initialization
    void Start()
    {
        switch (GameController.Difficulty)
        {
            default:
            case Difficulty.EASY:
                break;
            case Difficulty.NORMAL:
                projectileCount = projectileCount + 1;
                break;
            case Difficulty.HARD:
                projectileCount = projectileCount + 2;
                break;
        }
    }

    // Update is called once per frame
    void Update()
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

    protected IEnumerator Shoot()
    {
        _isShooting = true;

        yield return new WaitForSeconds(shootInterval);

        var standardAngle = (shootAtPlayer) ? Util.AngleFromVector(Util.Player.transform.position - transform.position) : Random.Range(0f, 360f);

        for (int i = (int)(0 - projectileCount / 2f); i < projectileCount / 2; i++)
        {
            var projectile = Instantiate(prefab);

            var offset = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
            var finalAngle = standardAngle + i * (totalSpreadAngle / projectileCount);
            Debug.Log(finalAngle);
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
}
