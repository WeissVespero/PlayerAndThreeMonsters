using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameControl : MonoBehaviour
{
    public int NumberOfBullets;

    [SerializeField] private Player _player;
    [SerializeField] private MutantManager _enemyManger;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _ammoCounter;

    private void Start()
    {
        _ammoCounter.text = NumberOfBullets.ToString();
        if (NumberOfBullets > 0) _player.HaveAmmo = true;
        Subscribe();
    }

    private void Subscribe()
    {
        _player.OnShoot += AmmoCount;

        _enemyManger.OnMutantAttack += DamagePlayer;
        _enemyManger.OnAllMutantsDeath += AllMutantsDeath;
    }

    private void AmmoCount()
    {
        NumberOfBullets--;
        _ammoCounter.text = NumberOfBullets.ToString();
        if (NumberOfBullets <= 0)
        {
            _player.HaveAmmo = false;
        }
    }

    private void AllMutantsDeath()
    {
        _gameOverText.text = "All mutants terminated";
        _gameOverPanel.SetActive(true);
    }

    private void DamagePlayer(float damage)
    {
        _player.Damage(damage);
        if (_player.Health <= 0)
        {
            _gameOverText.text = "Player is DEAD";
            _gameOverPanel.SetActive(true);
        }
    }
}
