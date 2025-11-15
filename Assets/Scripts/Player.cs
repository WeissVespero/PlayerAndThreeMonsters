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

    public Bullet bulletPrefab;
    public Transform firePoint;

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
        Shoot();
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.SetBulletDamage(AttackForce);
    }
}
