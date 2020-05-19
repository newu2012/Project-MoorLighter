﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveItem : MonoBehaviour, IPointerExitHandler
{
    public GameObject fastAccess;
    private bool isMoving = false;
    public GameObject Player;
    private bool isChanged = false;

    void OnMouseDown()
    {
        if (this.GetComponent<Image>().sprite != null )
        {
            isMoving = true;
            fastAccess.GetComponent<FastAccess>().ChangeColor();
            isChanged = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
	{
        if (isChanged)
        {
            isMoving = false;
            fastAccess.GetComponent<FastAccess>().ChangeBaseColor();
            isChanged = false;
        }
	}

    void Update()
    {
        var alpha = 0;
        if (isMoving)
            alpha = GetAlpha();
        if (alpha == 0)
            return;
        var worked = fastAccess.GetComponent<FastAccess>().ChangeAlpha(alpha, gameObject);
        if (worked)
        {
            var index = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());
            var type = gameObject.name.Substring(5, gameObject.name.Length - 6);
            Item item;
            if (type.CompareTo("Resource") == 0)
            {
                item = Player.GetComponent<CharacterInventory>().inventoryResourceItems[index];
                Player.GetComponent<CharacterInventory>().inventoryResourceItems.RemoveAt(index);
            }
            else if (type.CompareTo("Equipment") == 0)
            {
                item = Player.GetComponent<CharacterInventory>().inventoryEquipmentItems[index];
                Player.GetComponent<CharacterInventory>().inventoryEquipmentItems.RemoveAt(index);
            }
            else
            {
                item = Player.GetComponent<CharacterInventory>().inventoryConstructionsItems[index];
                Player.GetComponent<CharacterInventory>().inventoryConstructionsItems.RemoveAt(index);
            }
            this.GetComponent<ShowInformation>().Show();
            fastAccess.GetComponent<FastAccess>().ChangeBaseColor();
            Player.GetComponent<CharacterInventory>().inventoryPanel.GetComponent<InventoryPanel>().UpdatePanel(index, type);
            Player.GetComponent<CharacterFastAccess>().AddToArray(item, alpha - 1);
            
            if (item.Item_Type == Item.ItemType.Equipment &&
                item.Equipment_Type == Item.EquipmentType.Armor)
                Player.GetComponent<PlayerController>().ChangeArmor(item.Armor, true);
            else if (item.Item_Type == Item.ItemType.Equipment &&
                item.Equipment_Type == Item.EquipmentType.Weapon)
                Player.GetComponent<PlayerController>().ChangeDamage(item.Damage, true);
        }
    }

    private int GetAlpha()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
                return 1;
            else if(Input.GetKeyDown(KeyCode.Alpha2))
                return 2;           
            else if(Input.GetKeyDown(KeyCode.Alpha3))
                return 3;
            else if(Input.GetKeyDown(KeyCode.Alpha4))
                return 4;
            else if(Input.GetKeyDown(KeyCode.Alpha5))
                return 5;
            else if(Input.GetKeyDown(KeyCode.Alpha6))
                return 6;
            else if(Input.GetKeyDown(KeyCode.Alpha7))
                return 7;
            else if(Input.GetKeyDown(KeyCode.Alpha8))
                return 8;
            else if(Input.GetKeyDown(KeyCode.Alpha9))
                return 9;
        return 0;
    }
}
