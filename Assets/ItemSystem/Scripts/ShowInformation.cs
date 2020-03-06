using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ShowInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [FormerlySerializedAs("Player")] public GameObject player;
    public GameObject Name;
    [FormerlySerializedAs("Image")] public GameObject image;
    [FormerlySerializedAs("Count")] public GameObject count;
    [FormerlySerializedAs("MaxInInventary")] public GameObject maxInInventory;
    [FormerlySerializedAs("EquipmentType")] public GameObject equipmentType;
    [FormerlySerializedAs("Damage")] public GameObject damage;
    [FormerlySerializedAs("Armor")] public GameObject armor;
    [FormerlySerializedAs("Fraction")] public GameObject fraction;

    public void Start()
    {
        Name.GetComponent<Text>().text = null;
        count.GetComponent<Text>().text = null;
        maxInInventory.GetComponent<Text>().text = null;
        equipmentType.GetComponent<Text>().text = null;
        damage.GetComponent<Text>().text = null;
        armor.GetComponent<Text>().text = null;
        fraction.GetComponent<Text>().text = null;
        image.GetComponent<Image>().sprite = null;
        image.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        var objectName = gameObject.name;
        var type = objectName.Substring(5, objectName.Length - 6);
        var count = int.Parse(objectName[objectName.Length - 1].ToString());
        
        if (type == Item.ItemType.Resource.ToString() && count > player.GetComponent<CharacterInventory>().inventoryResourceItems.Count ||
            type == Item.ItemType.Equipment.ToString() && count > player.GetComponent<CharacterInventory>().inventoryEquipmentItems.Count ||
            type == Item.ItemType.Constructions.ToString() && count > player.GetComponent<CharacterInventory>().inventoryConstructionsItems.Count)
            return;
        
        Item currentItem;
        if (type == Item.ItemType.Resource.ToString())
            currentItem = player.GetComponent<CharacterInventory>().inventoryResourceItems[count];
        else if (type == Item.ItemType.Equipment.ToString())
        {
            currentItem = player.GetComponent<CharacterInventory>().inventoryEquipmentItems[count];
            equipmentType.GetComponent<Text>().text = Translate.TranslateWord(currentItem.Equipment_Type.ToString());
            if (currentItem.Equipment_Type == Item.EquipmentType.Armor)
                armor.GetComponent<Text>().text = "Защита: " + currentItem.Armor.ToString();
            else
                damage.GetComponent<Text>().text = "Урон: " + currentItem.Damage.ToString();
        }
        else
            currentItem = player.GetComponent<CharacterInventory>().inventoryConstructionsItems[count];

        Name.GetComponent<Text>().text = currentItem.ItemName;
        image.GetComponent<Image>().sprite = currentItem.ItemImage;
        image.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        this.count.GetComponent<Text>().text = currentItem.Count.ToString();
        maxInInventory.GetComponent<Text>().text = currentItem.MaxInInventory.ToString();
        fraction.GetComponent<Text>().text = "/";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Start();
    }
}
