using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _partToTurn;
    private float _speed = 2f;

    public Bullet BulletPrefab;
    public Transform FirePoint;

    public bool HaveAmmo = true;

    public event Action OnShoot;

    private void OnMove(InputValue value)
    {
        var vector = value.Get<Vector2>();
        var viewDirection = _partToTurn.localScale;
        _rigidbody.velocity = vector * _speed;
        if (vector.x > 0 && viewDirection.x < 0)
        {
            viewDirection.x = -viewDirection.x;
            _partToTurn.localScale = viewDirection;
        }
        if (vector.x < 0 && viewDirection.x > 0)
        {
            viewDirection.x = -viewDirection.x;
            _partToTurn.localScale = viewDirection;
        }
    }

    private void OnFire(InputValue value)
    {
        if (HaveAmmo)
        {
            OnShoot?.Invoke();
            Shoot();
        }
    }

    void Shoot()
    {
        var viewDirection = BulletPrefab.transform.localScale;
        viewDirection.x *= - Mathf.Sign(_partToTurn.transform.localScale.x);
       
        var bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        bullet.transform.localScale = viewDirection;
        bullet.SetBulletDamage(AttackForce);
    }
}
