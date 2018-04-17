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

    [SerializeField]
    private float _boundsWidth = 15f;
    [SerializeField]
    private float _boundsHeight = 10f;
    [SerializeField]
    private Vector2 _centerPoint = Vector2.zero;

    private float _scrollSpeed = 400f;
    [SerializeField]
    private float _zoomMin = 4f;
    [SerializeField]
    private float _zoomMax = 10f;

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
                Vector3 delta = _target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, transform.position.z));
                Vector3 destination = transform.position + delta;

                transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, _zDepth);
            }
            // When there is a target and there is shaking, dont damp the motion
            else if (_target && _isShaking)
            {
                transform.position = new Vector3(_target.position.x + _screenShakeAmount.x, _target.position.y + _screenShakeAmount.y, _zDepth);
            }
            // Revert to the default target when there is none
            else if (!_target)
            {
                _target = _standardCameraObject;
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f && GameController.GameState == GameState.PLAYING)
            {
                Camera.main.orthographicSize -= scroll * Time.deltaTime * _scrollSpeed;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _zoomMin, _zoomMax);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_centerPoint, new Vector3(_boundsWidth, _boundsHeight));
    }
}
