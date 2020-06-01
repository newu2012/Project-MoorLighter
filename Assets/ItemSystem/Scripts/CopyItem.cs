using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CopyItem
{
    public static Item Copy(Item item)
    {
        var result = ScriptableObject.CreateInstance<Item>();
        result.ItemName = item.ItemName;
        result.ItemNameEng = item.ItemNameEng;
        result.ItemImage = item.ItemImage;
        result.Item_Type = item.Item_Type;
        result.Equipment_Type = item.Equipment_Type;
        result.Damage = item.Damage;
        result.Armor = item.Armor;
        result.MaxInInventory = item.MaxInInventory;
        result.Count = item.Count;
        result.RecipeEng = item.RecipeEng;
        result.RecipeRus = item.RecipeRus;
        return result;
    }
}
