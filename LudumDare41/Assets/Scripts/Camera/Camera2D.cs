using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _dampTime = 0.15f;

    [SerializeField]
    private float _intensity = 1f;

    [SerializeField]
    private bool _manualOverride = false;

    [SerializeField]
    private AnimationCurve _motionCurve;

    private Vector2 _screenShakeAmount = Vector2.zero;

    private Vector3 _velocity = Vector3.zero;
    private float _zDepth = 0f;

    [SerializeField]
    private Transform _standardCameraObject;

    private bool _isShaking = false;

    private void Awake()
    {
        _zDepth = Camera.main.transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_manualOverride && (GameController.GameState == GameState.PLAYING || GameController.GameState == GameState.CINEMATIC))
        {
            // Camera shake
            var intensity = _intensity / 10f;
            _screenShakeAmount = (_isShaking) ? new Vector2(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity)) : Vector2.zero;

            // When there is a target and no shake
            if (_target && !_isShaking)
            {
                var delta = _target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, transform.position.z));
                var destination = transform.position + delta;

                transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, _zDepth);
            }
            // When there is a target and there is shaking, dont damp the motion
            else if (_target && _isShaking)
            {
                transform.position = new Vector3(_screenShakeAmount.x, _target.position.y + _screenShakeAmount.y, _zDepth);
            }
            // Revert to the default target when there is none
            else if (!_target)
            {
                _target = _standardCameraObject;
            }
        }

        var y = transform.position.y;
        y = Mathf.Clamp(y, 0f, 100000f);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
