using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KraftPanel : MonoBehaviour
{
    public GameObject Player;
    public List<Item> KraftItems = new List<Item>();
    public List<List<(string, int)>> recipes = new List<List<(string, int)>>();
    public GameObject InventoryPanel;

    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        for (var i = 0; i < KraftItems.Count; i++)
        {
            var currentItem = GameObject.Find("Item" + i.ToString());            
            currentItem.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            currentItem.GetComponent<Image>().sprite = KraftItems[i].ItemImage;
            recipes.Add(ParseEngRecipe.Parse(KraftItems[i].RecipeEng));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            canvas.enabled = !canvas.enabled;
            if (InventoryPanel.GetComponent<Canvas>().enabled)
                InventoryPanel.GetComponent<Canvas>().enabled = false;
            if (canvas.enabled)
                UpdateImage();
        }
    }

    public void UpdateImage()
    {
        var allItems = Player.GetComponent<CharacterInventory>().AllItems;
        for (var i = 0; i < KraftItems.Count; i++)
        {
            var suffice = false;
            foreach (var recipe in recipes[i])  
            {
                foreach(var item in allItems)
                    if (item.ItemNameEng.Equals(recipe.Item1))
                        if (item.Count >= recipe.Item2)
                            suffice = true;
                        else 
                        {
                            suffice = false;
                            break;
                        }
                if (!suffice)
                    break;
            }
            if (!suffice)
                GameObject.Find("Item" + i.ToString()).GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            else
                GameObject.Find("Item" + i.ToString()).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
    }
}
