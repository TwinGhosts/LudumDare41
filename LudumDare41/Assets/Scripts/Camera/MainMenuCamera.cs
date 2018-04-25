using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;

    private void Update()
    {
        Camera.main.transform.position = new Vector3(0f, transform.position.y + _moveSpeed * Time.deltaTime, -10f);
    }
}
