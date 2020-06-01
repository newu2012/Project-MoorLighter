using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var currentItem = (Item)target;

        currentItem.ItemName = EditorGUILayout.TextField("Item name", currentItem.ItemName);
        currentItem.ItemNameEng = EditorGUILayout.TextField("Item name Eng", currentItem.ItemNameEng);
        currentItem.ItemImage = (Sprite)EditorGUILayout.ObjectField(currentItem.ItemImage, typeof(Sprite), GUILayout.Width(100), GUILayout.Height(100));
        currentItem.Item_Type = (Item.ItemType)EditorGUILayout.EnumPopup("Item type", currentItem.Item_Type);
        currentItem.MaxInInventory = EditorGUILayout.IntField("Max in inventory", currentItem.MaxInInventory);
        currentItem.Count = EditorGUILayout.IntField("Count", currentItem.Count);
        currentItem.RecipeRus = EditorGUILayout.TextField("Recipe rus", currentItem.RecipeRus);
        currentItem.RecipeEng = EditorGUILayout.TextField("Recipe Eng. Ex: \"Log 3;Rock 2\"", currentItem.RecipeEng);

        if (currentItem.Item_Type == Item.ItemType.Equipment)
        {
            currentItem.Equipment_Type = (Item.EquipmentType)EditorGUILayout.EnumPopup("Equipment Type", currentItem.Equipment_Type);
            if (currentItem.Equipment_Type == Item.EquipmentType.Armor)
                currentItem.Armor = EditorGUILayout.IntField("Armor", currentItem.Armor);
            else
                currentItem.Damage = EditorGUILayout.IntField("Damage", currentItem.Damage);
        }   
    }
}
