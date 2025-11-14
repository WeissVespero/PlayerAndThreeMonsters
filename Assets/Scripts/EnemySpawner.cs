using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject enemyPrefab;

    public List<Character> characterList = new List<Character>();

    private int _numberOfEnemies = 3;

    private float _spawnRangeX = 12f;
    private float _spawnRangeY = 12f;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            var prefab = characterList[Random.Range(0, characterList.Count)];
            Vector3 randomPosition = GenerateRandomSpawnPosition();
            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        float randomX = Random.Range(-_spawnRangeX / 2, _spawnRangeX / 2);
        float randomY = Random.Range(-_spawnRangeY / 2, _spawnRangeY / 2);
        return new Vector2(randomX, randomY);
    }
}
