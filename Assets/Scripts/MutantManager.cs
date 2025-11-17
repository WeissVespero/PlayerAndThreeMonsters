using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MutantManager : MonoBehaviour
{
    public List<Mutant> EnemiesList = new List<Mutant>(); // types of enemies

    private float _spawnRangeX = 12f;
    private float _spawnRangeY = 12f;

    private int _numberOfMutants = 3;
    private int _mutantCount;

    public event Action<float> OnMutantAttack; // when mutant is attacking
    public event Action OnAllMutantsDeath; // when all mutants dead

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < _numberOfMutants; i++)
        {
            var prefab = EnemiesList[UnityEngine.Random.Range(0, EnemiesList.Count)];
            Vector3 randomPosition = GenerateRandomSpawnPosition();
            var enemy = Instantiate(prefab, randomPosition, Quaternion.identity);
            enemy.OnAttack += MutantAttack;
            enemy.OnDeath += MutantDeath;
        }
        _mutantCount = _numberOfMutants;
    }

    private void MutantDeath(Mutant mutant)
    {
        _mutantCount--;
        mutant.OnAttack -= MutantAttack;
        mutant.OnDeath -= MutantDeath;
        Destroy(mutant.gameObject);
        if (_mutantCount == 0)
        {
            OnAllMutantsDeath?.Invoke();
        }
    }

    private void MutantAttack(float attackForce)
    {
        OnMutantAttack.Invoke(attackForce);
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        float randomX = UnityEngine.Random.Range(-_spawnRangeX / 2, _spawnRangeX / 2);
        float randomY = UnityEngine.Random.Range(-_spawnRangeY / 2, _spawnRangeY / 2);
        return new Vector2(randomX, randomY);
    }
}
