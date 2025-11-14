using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Character _player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _player.Damage(9f);
        }
    }
}
