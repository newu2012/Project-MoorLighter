using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Resource,
        Constructions
    }

    public enum EquipmentType
    {
        Weapon,
        Armor
    }

    public string ItemName;
    public string ItemNameEng;
    public Sprite ItemImage;
    public ItemType Item_Type;
    public EquipmentType Equipment_Type;
    public int Damage;
    public int Armor;
    public int MaxInInventory;
    public int Count;
    public string RecipeRus;
    public string RecipeEng;
}
