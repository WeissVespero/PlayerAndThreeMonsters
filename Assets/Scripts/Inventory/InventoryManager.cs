using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> ItemsList = new List<Item>();

    public InventorySlot[] InventorySlots;
    private InventoryItemData[] _inventoryItems;

    private float _spawnRangeX = 12f;
    private float _spawnRangeY = 12f;

    private int _numberOfItems = 7;

    void Awake()
    {
        _inventoryItems = new InventoryItemData[InventorySlots.Length];

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i].SlotIndex = i;
            InventorySlots[i].ClearSlot();
        }
        SpawnItems();
    }

    public void AllRemoveButtonsOff()
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i].RemoveButtonOff();
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < _numberOfItems; i++)
        {
            var prefab = ItemsList[UnityEngine.Random.Range(0, ItemsList.Count)];
            Vector3 randomPosition = GenerateRandomSpawnPosition();
            var item = Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }

    public bool AddItem(Item item, int count)
    {
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i] != null && _inventoryItems[i].Item.ItemName == item.ItemName)
            {
                print($"{i} is {_inventoryItems[i].Item}");
                _inventoryItems[i].Count += count;
                InventorySlots[i].UpdateSlot(item, _inventoryItems[i].Count);
                return true;
            }
        }

        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i] == null)
            {
                _inventoryItems[i] = new InventoryItemData { Item = item, Count = count };
                InventorySlots[i].UpdateSlot(item, count);
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _inventoryItems.Length || _inventoryItems[slotIndex] == null)
        {
            return;
        }

        _inventoryItems[slotIndex] = null;

        InventorySlots[slotIndex].ClearSlot();
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        float randomX = UnityEngine.Random.Range(-_spawnRangeX / 2, _spawnRangeX / 2);
        float randomY = UnityEngine.Random.Range(-_spawnRangeY / 2, _spawnRangeY / 2);
        return new Vector2(randomX, randomY);
    }
}
