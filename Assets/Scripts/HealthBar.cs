using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;

    public void SetMaxHealth(int health)
    {
        fill.fillAmount = health;
    }

    public void SetHealth(int health)
    {
        fill.fillAmount = health;
    }
}
