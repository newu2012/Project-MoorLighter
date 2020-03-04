using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Canvas canvas;
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
            canvas.enabled = !canvas.enabled;
    }

    public void AddItem(Item item, int position)
    {
        var nameImage = "Image" + item.Item_Type.ToString() + position.ToString();
        var gameO = GameObject.Find(nameImage).GetComponent<Image>();
        gameO.color = new Color(255, 255, 255, 1);
        gameO.sprite = item.ItemImage;
    }
}
