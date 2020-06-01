using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveFastAccess : MonoBehaviour
{
    public GameObject Player;
    public GameObject fastAccess;
    void OnMouseDown()
    {
        if (this.GetComponent<Image>().sprite != null )
        {
            var index = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());
            var currentItem = Player.GetComponent<CharacterFastAccess>().items[index];
            var list = GetList(currentItem);
            if (list.Count > 7)
                return;
            gameObject.GetComponent<Image>().sprite = null;
            Player.GetComponent<CharacterFastAccess>().items[index] = null;
            Player.GetComponent<CharacterInventory>().AddItem(currentItem);
            fastAccess.GetComponent<FastAccess>().ChangeBaseColor();

            if (currentItem.Item_Type == Item.ItemType.Equipment &&
                currentItem.Equipment_Type == Item.EquipmentType.Armor)
                Player.GetComponent<PlayerController>().ChangeArmor(currentItem.Armor, false);
            else if (currentItem.Item_Type == Item.ItemType.Equipment &&
                currentItem.Equipment_Type == Item.EquipmentType.Weapon)
                Player.GetComponent<PlayerController>().ChangeDamage(currentItem.Damage, false);
        }
    }

    public List<Item> GetList(Item currentItem)
    {
        if (currentItem.Item_Type == Item.ItemType.Resource)
            return Player.GetComponent<CharacterInventory>().inventoryResourceItems;
        else if (currentItem.Item_Type == Item.ItemType.Equipment)
            return Player.GetComponent<CharacterInventory>().inventoryEquipmentItems;
        else
            return Player.GetComponent<CharacterInventory>().inventoryConstructionsItems;
    }
}
