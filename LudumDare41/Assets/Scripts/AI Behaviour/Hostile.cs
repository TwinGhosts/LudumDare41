using UnityEngine;

public class Hostile : MonoBehaviour
{
    private Color _currentColor = Color.white;
    private float _colorProgress = 1f;

    private void Update()
    {
        if (_colorProgress < 1f)
        {
            _currentColor = Color.Lerp(_currentColor, Color.white, _colorProgress);
            GetComponent<SpriteRenderer>().color = _currentColor;
            _colorProgress += 0.025f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ProjectileBase>())
        {
            if (!other.GetComponent<ProjectileBase>().hitPlayer)
            {
                GetComponent<UnitBase>().Stats.Damage(other.GetComponent<ProjectileBase>().Damage);
                if (other.GetComponent<TransferPoison>())
                {
                    var a = gameObject.AddComponent<PoisonEffectPlayer>();
                    a.damagePerTick = 3f;
                    a.duration = 4f;
                    a.nrOfTicks = 6;
                }

                _currentColor = CUtil.ColorHolder.red;
                _colorProgress = 0f;
                Destroy(other.gameObject);
            }
        }
    }
}
