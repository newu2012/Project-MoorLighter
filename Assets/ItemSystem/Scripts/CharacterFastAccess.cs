using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFastAccess : MonoBehaviour
{
    public Item[] items = new Item[9];
    public GameObject fastAccess;
    
    public void AddToArray(Item item, int index)
    {
        items[index] = CopyItem(item);
    }

    public Item CopyItem(Item item)
    {
        var result = ScriptableObject.CreateInstance<Item>();
        result.ItemName = item.ItemName;
        result.ItemImage = item.ItemImage;
        result.Item_Type = item.Item_Type;
        result.Equipment_Type = item.Equipment_Type;
        result.Damage = item.Damage;
        result.Armor = item.Armor;
        result.MaxInInventory = item.MaxInInventory;
        result.Count = item.Count;
        return result;
    }
}
