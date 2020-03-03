﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;

    public void SetHealth(float health)
    {
        fill.fillAmount = health;
    }
}
