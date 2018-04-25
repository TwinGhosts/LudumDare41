using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledProjectile : ProjectileBase
{
    [SerializeField]
    private Vector2 _direction = Vector2.zero;
    protected Vector2 dir;

    private bool _isPlayer = false;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_direction != Vector2.zero)
        {
            transform.up = dir;
            transform.rotation = Quaternion.Euler(0f, 0f, Util.AngleFromVector(dir) - 90f);
            transform.position += (Vector3)dir.normalized * _speed * Time.deltaTime;
        }
    }

    public void SetAngle(float angle)
    {
        _direction = Util.VectorFromAngle(angle);
        dir = (_isPlayer) ? (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position : _direction;
    }

    public void SetDirection(Vector2 dir)
    {
        _direction = dir;
        dir = (_isPlayer) ? (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position : _direction;
    }

    public void GoToMouse(bool isP)
    {
        _isPlayer = isP;
        dir = (_isPlayer) ? (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position : _direction;
    }
}
