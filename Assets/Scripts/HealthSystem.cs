using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    //[HideInInspector] public int currentHealth;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject resourcePrefab;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            if (resourcePrefab == null)
                return;
            Instantiate(resourcePrefab, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth/(float)maxHealth);
        Debug.Log(damage + " damage taken");
        Debug.Log(currentHealth/(float)maxHealth*100 + " % health now");
    }
}