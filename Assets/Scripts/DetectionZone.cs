using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private Mutant _mutant;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Передаем сигнал, что игрок замечен
            _mutant.SetPlayerDetected(other.transform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Передаем сигнал, что игрок потерян
            _mutant.SetPlayerDetected(null, false);
        }
    }
}
