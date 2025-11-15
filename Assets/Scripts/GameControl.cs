using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private EnemyManager _enemyManger;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        _enemyManger.OnEnemyAttack += DamagePlayer;
    }

    private void DamagePlayer(float damage)
    {
        _player.Damage(damage);
        if (_player.Health <= 0)
        {
            _gameOverText.text = "Player is DEAD";
            _gameOverPanel.SetActive(true);
            print("player Dead");
        }
    }
}
