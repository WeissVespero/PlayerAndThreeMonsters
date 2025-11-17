using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;

    [SerializeField] private Rigidbody2D _rb;

    private float _damage;
            
    void Start()
    {
        _rb.velocity = transform.right * Speed * Mathf.Sign(transform.localScale.x);
    }

    public void SetBulletDamage(float damage)
    {
        _damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Mutant>();
            enemy.MutantDamage(_damage);           
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
