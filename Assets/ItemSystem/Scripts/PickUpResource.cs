using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpResource : MonoBehaviour
{
    public Item currentItem;
    void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.transform.tag == "Player") 
		{
			obj.GetComponent<CharacterInventory>().AddItem(currentItem);
			Destroy(gameObject);
		}
	}
}
