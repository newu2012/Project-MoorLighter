using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Player;
    public GameObject Name;
    public GameObject Image;
    public GameObject Count;
    public GameObject MaxInInventary;
    public GameObject EquipmentType;
    public GameObject Damage;
    public GameObject Armor;
    public GameObject Fraction;

    public void Start()
    {
        Name.GetComponent<Text>().text = null;
        Count.GetComponent<Text>().text = null;
        MaxInInventary.GetComponent<Text>().text = null;
        EquipmentType.GetComponent<Text>().text = null;
        Damage.GetComponent<Text>().text = null;
        Armor.GetComponent<Text>().text = null;
        Fraction.GetComponent<Text>().text = null;
        Image.GetComponent<Image>().sprite = null;
        Image.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        var objectName = gameObject.name;
        var type = objectName.Substring(5, objectName.Length - 6);
        var count = int.Parse(objectName[objectName.Length - 1].ToString());
        
        if (type == Item.ItemType.Resource.ToString() && count > Player.GetComponent<CharacterInventary>().inventaryResourceItems.Count ||
            type == Item.ItemType.Equipment.ToString() && count > Player.GetComponent<CharacterInventary>().inventaryEquipmentItems.Count ||
            type == Item.ItemType.Constructions.ToString() && count > Player.GetComponent<CharacterInventary>().inventaryConstructionsItems.Count)
            return;
        
        Item currentItem;
        if (type == Item.ItemType.Resource.ToString())
            currentItem = Player.GetComponent<CharacterInventary>().inventaryResourceItems[count];
        else if (type == Item.ItemType.Equipment.ToString())
        {
            currentItem = Player.GetComponent<CharacterInventary>().inventaryEquipmentItems[count];
            EquipmentType.GetComponent<Text>().text = Translate.TranslateWord(currentItem.Equipment_Type.ToString());
            if (currentItem.Equipment_Type == Item.EquipmentType.Armor)
                Armor.GetComponent<Text>().text = "Защита: " + currentItem.Armor.ToString();
            else
                Damage.GetComponent<Text>().text = "Урон: " + currentItem.Damage.ToString();
        }
        else
            currentItem = Player.GetComponent<CharacterInventary>().inventaryConstructionsItems[count];

        Name.GetComponent<Text>().text = currentItem.ItemName;
        Image.GetComponent<Image>().sprite = currentItem.ItemImage;
        Image.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        Count.GetComponent<Text>().text = currentItem.Count.ToString();
        MaxInInventary.GetComponent<Text>().text = currentItem.MaxInInventory.ToString();
        Fraction.GetComponent<Text>().text = "/";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Start();
    }
}
