using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
	public Slider mpSlider;

	public void SetHUD(BattleUnit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
		mpSlider.maxValue = unit.maxMP;
		mpSlider.value = unit.currentMP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

	public void setMP(int mp)
	{
		mpSlider.value = mp;
	}

}
