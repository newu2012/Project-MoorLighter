using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Canvas canvas;
    public GameObject KraftPanel;
    public GameObject Player;
    // Start is called before the first frame update
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvas.enabled = !canvas.enabled;
            if (KraftPanel.GetComponent<Canvas>().enabled)
                KraftPanel.GetComponent<Canvas>().enabled = false;
        }
    }

    public void AddItem(Item item, int position)
    {
        var nameImage = "Image" + item.Item_Type.ToString() + position.ToString();
        var gameO = GameObject.Find(nameImage).GetComponent<Image>();
        gameO.color = new Color(255, 255, 255, 1);
        gameO.sprite = item.ItemImage;
    }

    public void UpdatePanel(string type)
    {
        for (var i = 0; i < 7; i++)
        {
            var gameOCurrent = GameObject.Find("Image" + type + i.ToString()).GetComponent<Image>();
            var list = GetList(type);
            if (list.Count <= i)
            {
                gameOCurrent.sprite = null;
                continue;
            }
            gameOCurrent.sprite = list[i].ItemImage;
        }
    }

    public List<Item> GetList(string type)
    {
        if (type.Equals("Resource"))
            return Player.GetComponent<CharacterInventory>().inventoryResourceItems;
        else if (type.Equals("Equipment"))
            return Player.GetComponent<CharacterInventory>().inventoryEquipmentItems;
        else 
            return Player.GetComponent<CharacterInventory>().inventoryConstructionsItems;
    }
}
