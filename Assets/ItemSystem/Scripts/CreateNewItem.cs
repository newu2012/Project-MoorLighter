using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewItem : MonoBehaviour
{
    public GameObject Player;
    public GameObject KraftPanel;
    public GameObject InventoryPanel;
    void OnMouseDown()
    {
        if (!KraftPanel.GetComponent<Canvas>().enabled)
            return;           
        if (GetComponent<Image>().color == new Color(255, 255, 255, 1))
        {
            var index = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());
            foreach (var recipe in KraftPanel.GetComponent<KraftPanel>().recipes[index])
                Remove(recipe, KraftPanel.GetComponent<KraftPanel>().KraftItems[index]);
            Player.GetComponent<CharacterInventory>().AddItem(KraftPanel.GetComponent<KraftPanel>().KraftItems[index]);
        }
    }

    void Remove((string, int) recipe, Item result)
    {
        var type = result.Item_Type;

        if (type == Item.ItemType.Constructions &&
            !CanAddToList(Player.GetComponent<CharacterInventory>().inventoryConstructionsItems, result.ItemNameEng))
                return;
        else if (type == Item.ItemType.Equipment && 
            !CanAddToList(Player.GetComponent<CharacterInventory>().inventoryEquipmentItems, result.ItemNameEng))
            {
                Debug.Log("Hi");
                return;
            }
        else if (!CanAddToList(Player.GetComponent<CharacterInventory>().inventoryResourceItems, result.ItemNameEng))
                return;

        var list = GetList(recipe.Item1);
        var typeNewList = list[0].Item_Type;
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].ItemNameEng.Equals(recipe.Item1))
            {
                list[i].Count -= recipe.Item2;
                if (list[i].Count == 0)
                    list.Remove(list[i]);
            }
        }

        foreach (var item in Player.GetComponent<CharacterInventory>().AllItems)
            if (item.ItemNameEng.Equals(recipe.Item1))
                item.Count -= recipe.Item2;
        
        InventoryPanel.GetComponent<InventoryPanel>().UpdatePanel(typeNewList.ToString());
        InventoryPanel.GetComponent<InventoryPanel>().UpdatePanel(type.ToString());
        KraftPanel.GetComponent<KraftPanel>().UpdateImage();
    }

    List<Item> GetList(string itemName)
    {
        var type = Item.ItemType.Constructions;
        foreach (var item in Player.GetComponent<CharacterInventory>().AllItems)
            if (item.ItemNameEng.Equals(itemName))
            {
                type = item.Item_Type;
                break;
            }
        if (type == Item.ItemType.Resource)
            return Player.GetComponent<CharacterInventory>().inventoryResourceItems;
        if (type == Item.ItemType.Equipment)
            return Player.GetComponent<CharacterInventory>().inventoryEquipmentItems;
        return Player.GetComponent<CharacterInventory>().inventoryConstructionsItems;
    }

    bool CanAddToList(List<Item> list, string name)
    {
        if (list.Count < 7)
            return true;
        for (var i = list.Count - 1; i >= 0; i--)
            if (list[i].ItemNameEng.Equals(name))
                if (list[i].Count < list[i].MaxInInventory)
                    return true;
                else
                    return false;
        return false;
    }
}
