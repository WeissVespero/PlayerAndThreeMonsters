using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool IsActive = true;

    public float MaxHealth;
    public float Health;
    public float AttackForce;
    private float _damageResist = 1; //coefficient of resistence. If 1, damage = attack force
    private float _initialScaleX = .25f; // initial X scale of fill bar

    [SerializeField] private Transform _healthBarFill;

    public void HealthBarUpdate()
    {
        var scale = _healthBarFill.localScale;
        scale.x = _initialScaleX * Health / MaxHealth;
        _healthBarFill.localScale = scale;
    }

    public void Damage(float damage)
    {
        Health -= damage * _damageResist;
        if (Health <= 0)
        {
            //death of character
            Health = 0;
            DeathInvoke();
        }
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        HealthBarUpdate();
    }

    public virtual void DeathInvoke()
    {
        print($"The {name} is dead");
    }
}
