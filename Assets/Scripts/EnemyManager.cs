using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Mutant> enemiesList = new List<Mutant>(); // types of enemies

    private float _spawnRangeX = 12f;
    private float _spawnRangeY = 12f;

    private int _numberOfEnemies = 3;

    public event Action<float> OnEnemyAttack; // when mutant is attacking

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            var prefab = enemiesList[UnityEngine.Random.Range(0, enemiesList.Count)];
            Vector3 randomPosition = GenerateRandomSpawnPosition();
            var enemy = Instantiate(prefab, randomPosition, Quaternion.identity);
            enemy.OnAttack += MutantAttack;
        }
    }

    private void MutantAttack(float attackForce)
    {
        OnEnemyAttack.Invoke(attackForce);
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        float randomX = UnityEngine.Random.Range(-_spawnRangeX / 2, _spawnRangeX / 2);
        float randomY = UnityEngine.Random.Range(-_spawnRangeY / 2, _spawnRangeY / 2);
        return new Vector2(randomX, randomY);
    }
}
