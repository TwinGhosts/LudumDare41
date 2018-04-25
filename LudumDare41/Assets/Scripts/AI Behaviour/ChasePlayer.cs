using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public bool attackInRange = false;
    public float attackSpeed = 1f;
    public float movementSpeed = 3f;

    private bool _isInRange = false;
    private bool _isAttacking = false;

    private float startingZ;

    private void Awake()
    {
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Util.Player.transform.position - transform.position).magnitude > 1.2f)
        {
            _isInRange = false;
            transform.up = (Util.Player.transform.position - transform.position).normalized;
            transform.position += transform.up * Time.deltaTime * movementSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, startingZ);
        }
        else
        {
            _isInRange = true;

            transform.up = (Util.Player.transform.position - transform.position).normalized;
            transform.position += transform.up * Time.deltaTime * movementSpeed / 2f;
            transform.position = new Vector3(transform.position.x, transform.position.y, startingZ);

            if (attackInRange && !_isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(attackSpeed);
        if (_isInRange)
        {
            Util.Player.Damage(GetComponent<UnitBase>().Stats.attackPower);
        }
        _isAttacking = false;
    }
}
