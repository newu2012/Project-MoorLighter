using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public int maxMP;
	public int currentMP;

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		return currentHP <= 0;
	}

	public bool CanSpendMana()
	{
		return currentMP >= 5;
	}
	public bool SpendMana()
	{
		currentMP -= 5;

		return currentMP <= 0;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void RegenMana(int amount)
	{
		currentMP += amount;
		if (currentMP > maxMP)
			currentMP = maxMP;
	}
}
