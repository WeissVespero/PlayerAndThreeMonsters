using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Mutant : Character
{
    public float MoveSpeed;

    public float AttackRange;

    [SerializeField] private Transform _partToTurn;

    private Transform _playerTarget;
    private bool _isPlayerInRange = false;
    private bool _isAtacking;    

    public event Action<float> OnAttack;
    public event Action<Mutant> OnDeath;

    void Update()
    {
        if (_isPlayerInRange && _playerTarget != null && !_isAtacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerTarget.position);

            if (distanceToPlayer > AttackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    public void SetPlayerDetected(Transform target, bool detected)
    {
        _isPlayerInRange = detected;
        _playerTarget = target;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = _playerTarget.position - transform.position;
        var viewDirection = _partToTurn.localScale;
        if (direction.x > 0 && viewDirection.x < 0)
        {
            viewDirection.x = -viewDirection.x;
            _partToTurn.localScale = viewDirection;
        }

        if (direction.x < 0 && viewDirection.x > 0)
        {
            viewDirection.x = -viewDirection.x;
            _partToTurn.localScale = viewDirection;
        }

        transform.position = Vector3.MoveTowards(transform.position, _playerTarget.position, MoveSpeed * Time.deltaTime);
    }

    public void MutantDamage(float damage)
    {
        Damage(damage);
        if (Health <= 0)
        {
            OnDeath?.Invoke(this);
        }
    }

    private IEnumerator Attack()
    {
        _isAtacking = true;
        OnAttack.Invoke(AttackForce);
        yield return new WaitForSeconds(1f); // 1 second to stop after attack
        _isAtacking = false;
    }
}
