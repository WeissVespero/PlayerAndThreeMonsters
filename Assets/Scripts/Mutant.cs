using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : Character
{
    public float MoveSpeed;

    public float AttackRange;

    private Transform _playerTarget;
    private bool _isPlayerInRange = false;
    private bool _isAtacking;

    [SerializeField] private Transform _partToTurn;

    public event Action<float> OnAttack;

    void Update()
    {
        if (_isPlayerInRange && _playerTarget != null && !_isAtacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerTarget.position);

            if (distanceToPlayer > AttackRange)
            {
                // Приближение
                MoveTowardsPlayer();
            }
            else
            {
                // Атака
                StartCoroutine(Attack());
            }
        }
    }

    public void SetPlayerDetected(Transform target, bool detected)
    {
        _isPlayerInRange = detected;
        _playerTarget = target;

        string state = detected ? "замечен" : "потерян";
        Debug.Log($"Игрок {state}!");
    }

    private void MoveTowardsPlayer()
    {
        // Вычисляем направление к игроку
        Vector3 direction = _playerTarget.position - transform.position;
        var viewDirection = _partToTurn.localScale;
        // Перемещаем монстра в этом направлении
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

        // TODO: Здесь можно добавить анимацию бега
    }


    private IEnumerator Attack()
    {
        _isAtacking = true;
        OnAttack.Invoke(AttackForce);
        yield return new WaitForSeconds(1f); // 1 second to stop after attack
        _isAtacking = false;
    }
}
