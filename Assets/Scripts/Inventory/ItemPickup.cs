using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Настройки")]
    public Item ItemData;
    public int ItemCount = 1;

    private InventoryManager _inventoryManager;

    void Start()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
        if (_inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _inventoryManager != null)
        {
            bool wasAdded = _inventoryManager.AddItem(ItemData, ItemCount);

            if (wasAdded)
            {
                Destroy(gameObject);
            }
        }
    }
}
