using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowKraftItemInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject player;
    public GameObject KraftPanel;
    public GameObject Name;
    public GameObject image;
    public GameObject equipmentType;
    public GameObject damage;
    public GameObject armor;
    public GameObject recipe;

    // Start is called before the first frame update
    void Start()
    {
        Name.GetComponent<Text>().text = null;
        equipmentType.GetComponent<Text>().text = null;
        damage.GetComponent<Text>().text = null;
        armor.GetComponent<Text>().text = null;
        image.GetComponent<Image>().sprite = null;
        image.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        recipe.GetComponent<Text>().text = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Show();
    }

    public void Show()
    {
        if (GetComponent<Image>().sprite == null)
            return;
        var count = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());

        Item currentItem = KraftPanel.GetComponent<KraftPanel>().KraftItems[count];
        
        if (currentItem.Item_Type == Item.ItemType.Equipment)
        {
            equipmentType.GetComponent<Text>().text = Translate.TranslateWord(currentItem.Equipment_Type.ToString());
            if (currentItem.Equipment_Type == Item.EquipmentType.Armor)
                armor.GetComponent<Text>().text = "Защита: " + currentItem.Armor.ToString();
            else
                damage.GetComponent<Text>().text = "Урон: " + currentItem.Damage.ToString();
        }

        Name.GetComponent<Text>().text = currentItem.ItemName;
        image.GetComponent<Image>().sprite = currentItem.ItemImage;
        image.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        recipe.GetComponent<Text>().text = currentItem.RecipeRus;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Start();
    }
}
