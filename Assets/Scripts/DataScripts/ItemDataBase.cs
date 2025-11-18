using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;

    [Header("Все предметы в игре")]
    public List<Item> allItems = new List<Item>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Item GetItemByID(string id)
    {
        return allItems.FirstOrDefault(item => item.ItemID == id);
    }
}
