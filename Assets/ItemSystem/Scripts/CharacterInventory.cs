using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterInventory : MonoBehaviour
{
    public Item[] InitialCharacterItems;
    [FormerlySerializedAs("inventaryPanel")] public GameObject inventoryPanel;
    [FormerlySerializedAs("inventaryResourceItems")] public List<Item> inventoryResourceItems = new List<Item>();
    [FormerlySerializedAs("inventaryEquipmentItems")] public List<Item> inventoryEquipmentItems = new List<Item>();
    [FormerlySerializedAs("inventaryConstructionsItems")] public List<Item> inventoryConstructionsItems = new List<Item>();
    // Start is called before the first frame update
    private void Start()
    {
        foreach (var item in InitialCharacterItems)
            AddItem(item);
    }

    public void AddItem(Item item)
    {
        switch (item.Item_Type)
        {
            case Item.ItemType.Resource:
                AddToList(item, inventoryResourceItems);
                break;
            case Item.ItemType.Equipment:
                AddToList(item, inventoryEquipmentItems);
                break;
            case Item.ItemType.Constructions:
                AddToList(item, inventoryConstructionsItems);
                break;
            default:
                break;
        }
    }

    public void AddToList(Item itemToAdd, List<Item> List)
    {
        foreach (var item in List)
            if (item.ItemName.CompareTo(itemToAdd.ItemName) == 0 && item.Count < item.MaxInInventory)
            {
                item.Count++;
                return;
            }
        if (List.Count < 7)
        {
            List.Add(CopyItem(itemToAdd));
            inventoryPanel.GetComponent<InventoryPanel>().AddItem(itemToAdd, List.Count - 1);
        }
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
