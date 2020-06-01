using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastAccess : MonoBehaviour
{
    public void ChangeColor()
    {        
        for (var i = 0; i < 9; i++)
        {
            var img = GameObject.Find("FastItem" + i.ToString()).GetComponent<Image>();
            if (img.sprite == null)
                img.color = new Color(255, 255, 255, 255);
        }
    }

    public void ChangeBaseColor()
    {
        for (var i = 0; i < 9; i++)
        {
            var img = GameObject.Find("FastItem" + i.ToString()).GetComponent<Image>();
            if (img.sprite == null)
                img.color = new Color(255, 255, 255, 0.4f);
        }
    }

    public bool ChangeAlpha(int alpha, GameObject go)
    {
        var img = GameObject.Find("FastItem" + (alpha - 1).ToString()).GetComponent<Image>();
        if (img.sprite == null)
        {
            img.sprite = go.GetComponent<Image>().sprite;
            go.GetComponent<Image>().sprite = null;
            return true;
        }
        return false;
    }
}
