using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFastAccess : MonoBehaviour
{
    public Item[] items = new Item[9];
    public GameObject fastAccess;
    
    public void AddToArray(Item item, int index)
    {
        items[index] = CopyItem.Copy(item);
    }
}
