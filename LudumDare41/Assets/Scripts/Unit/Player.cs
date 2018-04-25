using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : UnitBase
{
    [SerializeField]
    private SpriteRenderer _playerCharacterSprite;

    [SerializeField]
    private Sprite _warriorSprite;
    [SerializeField]
    private Sprite _archerSprite;

    [SerializeField]
    private Class _class = Class.RANGER;

    public UnityEvent OnLose;

    private Color _currentColor = Color.white;
    private float _colorProgress = 1f;
    private bool _isAttacking = false;

    public AngledProjectile _archerAbOneProjectile;
    public AngledProjectile _archerAbTwoProjectile;
    public AngledProjectile _archerAbThreeProjectile;

    public AngledProjectile _warriorAbOneProjectile;
    public AngledProjectile _warriorAbTwoProjectile;

    public bool isInvunerable = false;
    public bool isAlive = true;

    public bool isDying = false;

    protected override void Awake()
    {
        base.Awake();
        Util.Player = this;

        if (Util.PlayerStats == null)
        {
            Util.PlayerStats = Stats;

            Stats.maxHealth += Stats.levelUpHealth * Stats.level;
            Stats.maxMana += Stats.levelUpMana * Stats.level;

            Stats.health = Stats.maxHealth;
            Stats.mana = Stats.maxMana;
        }
        else
        {
            _stats = Util.PlayerStats;
        }

        switch (_class)
        {
            case Class.WARRIOR:
                gameObject.AddComponent<ClassWarrior>();
                _playerCharacterSprite.sprite = _warriorSprite;
                break;
            case Class.RANGER:
                gameObject.AddComponent<ClassArcher>();
                _playerCharacterSprite.sprite = _archerSprite;
                break;
        }

        Stats.maxHealth += Util.PlayerStats.levelUpHealth * Util.PlayerStats.level;
        Stats.maxMana += Util.PlayerStats.levelUpMana * Util.PlayerStats.level;

        Stats.health = Stats.maxHealth;
        Stats.mana = Stats.maxMana;
    }

    protected override void Update()
    {
        base.Update();

        if (isAlive)
        {
            Util.PlayerStats = Stats;
            Util.UIManager.UpdateUIProperties(this);
            if (GameController.GameState == GameState.PLAYING)
            {
                Movement();
                Aim();
            }

            if (_colorProgress < 1f)
            {
                _currentColor = Color.Lerp(_currentColor, Color.white, _colorProgress);
                GetComponent<SpriteRenderer>().color = _currentColor;
                _playerCharacterSprite.color = _currentColor;
                _colorProgress += 0.025f;
            }
        }
        else if (!isDying)
        {
            StartCoroutine(Lose());
        }
    }

    private IEnumerator Lose()
    {
        isDying = true;
        var ps = GetComponentInChildren<ParticleSystem>();
        _playerCharacterSprite.enabled = false;
        ps.Play();
#pragma warning disable CS0618 // Type or member is obsolete
        ps.loop = false;
#pragma warning restore CS0618 // Type or member is obsolete
        yield return new WaitForSeconds(1.25f);

        OnLose.Invoke();
        Time.timeScale = 0f;
        isDying = false;
    }

    protected virtual void Aim()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = ((Vector2)mousePos - (Vector2)transform.position).normalized;

        _playerCharacterSprite.transform.up = direction;
    }

    protected virtual void Movement()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        var range = 70f;

        var relRange = (range - -range) / 2f;

        var offset = range - relRange;

        var angles = transform.eulerAngles;
        var z = ((angles.z + 540) % 360) - 180 - offset;

        var rotationAddition = (-hInput * Stats.rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotationAddition);

        if (Mathf.Abs(z) > relRange)
        {
            angles.z = relRange * Mathf.Sign(z) + offset;
            transform.eulerAngles = angles;
        }

        var standardSpeed = Stats.maxMovementSpeed / 0.75f;
        var minSpeed = 1.75f;

        Stats.movementSpeed = Mathf.Clamp(vInput * Stats.maxMovementSpeed, minSpeed, Stats.maxMovementSpeed);

        if (vInput == 0f)
        {
            Stats.movementSpeed = Mathf.MoveTowards(Stats.movementSpeed, standardSpeed, 0.125f);
        }

        transform.position += transform.up * (Stats.movementSpeed * Time.deltaTime);
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, -6.75f, 6.75f),
                transform.position.y,
                transform.position.z
            );
    }

    public void Damage(float amount)
    {
        if (!isInvunerable)
        {
            _colorProgress = 0f;
            _currentColor = CUtil.ColorHolder.red;

            Stats.health -= amount;
            if (Stats.health <= 0f && !isDying)
            {
                StartCoroutine(Lose());
                Stats.maxMovementSpeed = 0.1f;
            }
        }
        else
        {
            _colorProgress = 0f;
            _currentColor = CUtil.ColorHolder.blue;
        }
    }
}

public enum Class
{
    WARRIOR,
    RANGER,
}
