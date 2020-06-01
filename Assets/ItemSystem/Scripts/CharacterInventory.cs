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
    public List<Item> AllItems = new List<Item>();
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
        var inList = false;
        foreach (var item in AllItems)
            if (item.ItemName.Equals(itemToAdd.ItemName))
            {
                item.Count += itemToAdd.Count;
                inList = true;
                break;
            }
        if (!inList)
            AllItems.Add(CopyItem.Copy(itemToAdd));

        foreach (var item in List)
            if (item.ItemName.CompareTo(itemToAdd.ItemName) == 0 && item.Count < item.MaxInInventory)
            {
                item.Count++;
                return;
            }
        if (List.Count < 7)
        {
            List.Add(CopyItem.Copy(itemToAdd));
            inventoryPanel.GetComponent<InventoryPanel>().AddItem(itemToAdd, List.Count - 1);
        }
    }
}
