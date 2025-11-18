using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedSlotData
{
    // Вместо ссылки на Item, сохраняем уникальный ID предмета
    public string itemID;
    public int count;

    public SavedSlotData(string id, int c)
    {
        itemID = id;
        count = c;
    }
}
