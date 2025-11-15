using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] private Rigidbody2D rb;

    private float _damage;
            
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Запускаем пулю вперёд (предполагаем, что пуля смотрит вправо)
        rb.velocity = transform.right * speed;
    }

    public void SetBulletDamage(float damage)
    {
        _damage = damage;
    }

    // Обработка столкновений
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем столкновение с объектом, помеченным тегом "Enemy"
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character>().Damage(_damage);
            print($"Damaged on {_damage}");
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
