using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    private float _speed = 2f;

    private void OnMove(InputValue value)
    {
        _rigidbody.velocity = value.Get<Vector2>() * _speed;
    }

    private void OnFire(InputValue value)
    {
        print(value);
    }
}
