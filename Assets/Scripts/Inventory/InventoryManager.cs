using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> ItemsList = new List<Item>();

    public InventorySlot[] InventorySlots;
    private InventoryItemData[] _inventoryItems;

    private float _spawnRangeX = 12f;
    private float _spawnRangeY = 12f;

    private int _numberOfItems = 7;

    private string _savePath;

    void Awake()
    {
        _inventoryItems = new InventoryItemData[InventorySlots.Length];

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i].SlotIndex = i;
            InventorySlots[i].ClearSlot();
        }
        SpawnItems();
        _savePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        LoadInventory();
    }

    void OnApplicationQuit()
    {
        SaveInventory();
    }

    public void SaveInventory()
    {
        InventorySaveData dataToSave = new InventorySaveData();

        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i] != null)
            {
                dataToSave.slots.Add(new SavedSlotData(_inventoryItems[i].Item.ItemID, _inventoryItems[i].Count));
            }
        }

        string json = JsonUtility.ToJson(dataToSave, true);

        File.WriteAllText(_savePath, json);
        Debug.Log($"Инвентарь сохранен в: {_savePath}");
    }

    public void LoadInventory()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);

            InventorySaveData loadedData = JsonUtility.FromJson<InventorySaveData>(json);

            for (int i = 0; i < _inventoryItems.Length; i++)
            {
                _inventoryItems[i] = null;
                InventorySlots[i].ClearSlot();
            }

            for (int i = 0; i < loadedData.slots.Count; i++)
            {
                SavedSlotData savedSlot = loadedData.slots[i];

                Item itemToLoad = ItemDataBase.Instance.GetItemByID(savedSlot.itemID);

                if (itemToLoad != null && i < _inventoryItems.Length)
                {
                    _inventoryItems[i] = new InventoryItemData { Item = itemToLoad, Count = savedSlot.count };
                    InventorySlots[i].UpdateSlot(itemToLoad, savedSlot.count);
                }
            }
            Debug.Log("Инвентарь загружен.");
        }
        else
        {
            Debug.LogWarning("Файл сохранения инвентаря не найден. Начинаем с пустого инвентаря.");
        }
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
            Vector3 randomPosition = GenerateRandomSpawnPosition();
            SpawnItemAtPlace(randomPosition);
        }
    }

    public void SpawnItemAtPlace(Vector3 position)
    {
        var prefab = ItemsList[UnityEngine.Random.Range(0, ItemsList.Count)];
        Instantiate(prefab, position, Quaternion.identity);
    }

    public bool AddItem(Item item, int count)
    {
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i] != null && _inventoryItems[i].Item.ItemName == item.ItemName)
            {
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
