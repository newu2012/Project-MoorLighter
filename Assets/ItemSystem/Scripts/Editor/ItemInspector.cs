using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Item currenItem = (Item)target;

        currenItem.ItemName = EditorGUILayout.TextField("Item name", currenItem.ItemName);
        currenItem.ItemImage = (Sprite)EditorGUILayout.ObjectField(currenItem.ItemImage, typeof(Sprite), GUILayout.Width(100), GUILayout.Height(100));
        currenItem.Item_Type = (Item.ItemType)EditorGUILayout.EnumPopup("Item type", currenItem.Item_Type);
        currenItem.MaxInInventory = EditorGUILayout.IntField("Max in inventary", currenItem.MaxInInventory);
        currenItem.Count = EditorGUILayout.IntField("Count", currenItem.Count);

        if (currenItem.Item_Type == Item.ItemType.Equipment)
        {
            currenItem.Equipment_Type = (Item.EquipmentType)EditorGUILayout.EnumPopup("Equipment Type", currenItem.Equipment_Type);
            if (currenItem.Equipment_Type == Item.EquipmentType.Armor)
                currenItem.Armor = EditorGUILayout.IntField("Armor", currenItem.Armor);
            else
                currenItem.Damage = EditorGUILayout.IntField("Damage", currenItem.Damage);
        }   
    }
}
