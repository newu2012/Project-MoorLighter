using System.Collections;
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
    public GameObject InventoryPanel;

    void OnMouseDown()
    {
        if (!InventoryPanel.GetComponent<Canvas>().enabled)
            return;
        if (this.GetComponent<Image>().sprite != null )
        {
            isMoving = true;
            fastAccess.GetComponent<FastAccess>().ChangeColor();
            isChanged = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
	{
        if (!InventoryPanel.GetComponent<Canvas>().enabled)
            return;
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
        var isLast = false;
        if (worked)
        {
            var index = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());
            var type = gameObject.name.Substring(5, gameObject.name.Length - 6);
            Item item;
            if (type.CompareTo("Resource") == 0)
            {
                item = Player.GetComponent<CharacterInventory>().inventoryResourceItems[index];
                Player.GetComponent<CharacterInventory>().inventoryResourceItems.RemoveAt(index);
                if (Player.GetComponent<CharacterInventory>().inventoryResourceItems.Count == 0)
                    isLast = true;
            }
            else if (type.CompareTo("Equipment") == 0)
            {
                item = Player.GetComponent<CharacterInventory>().inventoryEquipmentItems[index];
                Player.GetComponent<CharacterInventory>().inventoryEquipmentItems.RemoveAt(index);
                if (Player.GetComponent<CharacterInventory>().inventoryEquipmentItems.Count == 0)
                    isLast = true;
            }
            else
            {
                item = Player.GetComponent<CharacterInventory>().inventoryConstructionsItems[index];
                Player.GetComponent<CharacterInventory>().inventoryConstructionsItems.RemoveAt(index);
                if (Player.GetComponent<CharacterInventory>().inventoryConstructionsItems.Count == 0)
                    isLast = true;
            }           
            if (isLast)
                GetComponent<ShowInformation>().Start();
            else
                GetComponent<ShowInformation>().Show();
                
            fastAccess.GetComponent<FastAccess>().ChangeBaseColor();
            Player.GetComponent<CharacterInventory>().inventoryPanel.GetComponent<InventoryPanel>().UpdatePanel(type);            
            Player.GetComponent<CharacterFastAccess>().AddToArray(item, alpha - 1);
            
            if (item.Item_Type == Item.ItemType.Equipment &&
                item.Equipment_Type == Item.EquipmentType.Armor)
                Player.GetComponent<PlayerController>().ChangeArmor(item.Armor, true);
            else if (item.Item_Type == Item.ItemType.Equipment &&
                item.Equipment_Type == Item.EquipmentType.Weapon)
                Player.GetComponent<PlayerController>().ChangeDamage(item.Damage, true);

            foreach (var allItem in Player.GetComponent<CharacterInventory>().AllItems)
                if (allItem.ItemNameEng.Equals(item.ItemNameEng))
                {
                    allItem.Count -= item.Count;
                    if (allItem.Count == 0)
                        Player.GetComponent<CharacterInventory>().AllItems.RemoveAt(Player.GetComponent<CharacterInventory>().AllItems.IndexOf(allItem));
                    return;
                }
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
