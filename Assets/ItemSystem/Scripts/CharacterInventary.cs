using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventary : MonoBehaviour
{
    public Item[] InitialCharacterItems;
    public InventaryPanel inventaryPanel;
    public List<Item> inventaryResourceItems = new List<Item>();
    public List<Item> inventaryEquipmentItems = new List<Item>();
    public List<Item> inventaryConstructionsItems = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in InitialCharacterItems)
            AddItem(item);
    }

    public void AddItem(Item item)
    {
        if (item.Item_Type == Item.ItemType.Resource)
            AddToList(item, inventaryResourceItems);
        else if (item.Item_Type == Item.ItemType.Equipment)
            AddToList(item, inventaryEquipmentItems);
        else    
            AddToList(item, inventaryConstructionsItems);
    }

    public void AddToList(Item itemToAdd, List<Item> List)
    {
        foreach (var item in List)
            if (item.name.CompareTo(itemToAdd.name) == 0 && item.Count < item.MaxInInventory)
            {
                item.Count++;
                return;
            }
        if (List.Count < 7)
        {
            List.Add(itemToAdd);
            inventaryPanel.AddItem(itemToAdd, List.Count - 1);
        }
    }
}
