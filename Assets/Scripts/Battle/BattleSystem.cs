using System;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = System.Random;
using Transform = UnityEngine.Transform;

public enum BattleState { START, PLAYERTURN, PLAYER_CHOOSED_ACTION, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	public GameObject playerPrefab;
	private GameObject playerGO;
	public GameObject damagerPrefab;
	public GameObject weakPrefab;
	
	public GameObject enemyPrefab;
	private GameObject enemyGO;
	public Animator enemyAnimator;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	BattleUnit playerUnit;
	BattleUnit enemyUnit;

	public ParticleSystem[] effects;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	public GameObject dialoguePanel;
	public bool dialogueSwitch = true;

	public GameObject[] Panels;
	private int PanelIndex;

	private static readonly int Attacking = Animator.StringToHash("Attacking");
	
	// Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		Debug.Log("Setup Battle");
		
		playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<BattleUnit>();
		playerGO.transform.localScale = new Vector3(0.15f, 0.15f, 0);
		playerGO.transform.position = new Vector3(-6, -0.5f, 0);
		playerGO.GetComponent<UnityArmatureComponent>().animation.Play("dRINNKbeer", 1);
		
		//enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyGO = GameObject.Find("Volk");
		enemyUnit = enemyGO.GetComponent<BattleUnit>();
		//enemyGO.transform.localScale = new Vector3(0.25f, 0.25f, 0);
		//enemyGO.transform.position = new Vector3(6, -1f, 0);
		
		dialogueText.text = "Приближается дикий " + enemyUnit.unitName + "...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);
		
		yield return new WaitForSeconds(2f);
		Debug.Log("Before Battle");
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		/* */
		damagerPrefab.transform.position = playerGO.transform.position;
		playerGO.transform.position = new Vector3(-15, -1);
		damagerPrefab.GetComponent<UnityArmatureComponent>().animation.Play("Attack1", 1);
		/* */
		/* 
		weakPrefab.transform.position = playerGO.transform.position;
		playerGO.transform.position = new Vector3(-15, -1);
		weakPrefab.GetComponent<UnityArmatureComponent>().animation.Play("newAnimation", 1); 
		 */
		
		
		
		var damage = playerUnit.damage;
		var fullDamage = Mathf.RoundToInt(UnityEngine.Random.Range(damage * 0.5f, damage * 1.5f));
		bool isDead = enemyUnit.TakeDamage(fullDamage);
		ChangeDialogueState();
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "Атака прошла успешно!";
		
		yield return new WaitForSeconds(1f);
		
		dialogueText.text = fullDamage + " урона!";
		/* */
		playerGO.transform.position = damagerPrefab.transform.position;
		damagerPrefab.transform.position = new Vector3(-15, -1);
		/* */
		/* 
		playerGO.transform.position = weakPrefab.transform.position;
		weakPrefab.transform.position = new Vector3(-15, -1);
		 */
		yield return new WaitForSeconds(1f);
		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.PLAYER_CHOOSED_ACTION;
			StartCoroutine(EnemyTurn());
		}
	}
	
	IEnumerator PlayerShadowAttack()
	{
		damagerPrefab.transform.position = playerPrefab.transform.position;
		playerPrefab.transform.position = new Vector3(-15, -1);
		damagerPrefab.GetComponent<UnityArmatureComponent>().animation.Play("Attack2", 3);
		
		var damage = playerUnit.damage;
		var fullDamage = Mathf.RoundToInt(UnityEngine.Random.Range(damage * 0.5f, damage * 5f));
		bool isDead = enemyUnit.TakeDamage(fullDamage);
		ChangeDialogueState();
		playerUnit.currentMP = 0;
		playerHUD.setMP(playerUnit.currentMP);
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "Удачная теневая атака!";
		
		yield return new WaitForSeconds(1f);
		
		dialogueText.text = fullDamage + " урона! Ого!";
		playerPrefab.transform.position = damagerPrefab.transform.position;
		damagerPrefab.transform.position = new Vector3(-15, -1);
		yield return new WaitForSeconds(1f);
		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.PLAYER_CHOOSED_ACTION;
			StartCoroutine(EnemyTurn());
		}
	}
	

	IEnumerator EnemyTurn()
	{
		state = BattleState.ENEMYTURN;
		dialogueText.text = enemyUnit.unitName + " атакует!";
		enemyAnimator.SetFloat(Attacking, 5f);
		yield return new WaitForSeconds(1f);

		//Panels[1].GetComponent<GridLayoutGroup>().GetComponent<Button>().onClick = PlayerMagic(1, "fire");

		
		Instantiate(effects[0], playerBattleStation);
		var damage = enemyUnit.damage;
		var fullDamage = Mathf.RoundToInt(UnityEngine.Random.Range(damage * 0.5f, damage * 1.5f));
		bool isDead = playerUnit.TakeDamage(fullDamage);
		dialogueText.text = fullDamage + " урона!";
		playerHUD.SetHP(playerUnit.currentHP);
		playerHUD.setMP(playerUnit.currentMP);
		yield return new WaitForSeconds(2f);
		enemyAnimator.SetFloat(Attacking, 0f);
		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "Вы победили в битве!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "Вы проиграли в битве.";
		}
	}

	void PlayerTurn()
	{
		playerGO.GetComponent<UnityArmatureComponent>().animation.Play("PROSTOI", 0);
		Debug.Log("Player Turn");
		dialogueText.text = "Выберите действие:";
		ChangeDialogueState();
	}

	IEnumerator PlayerRestore(bool health, bool mana)
	{
		if (health)
			playerUnit.Heal(5);
		if (mana)
			playerUnit.RegenMana(5);
		ChangeDialogueState();
		playerHUD.SetHP(playerUnit.currentHP);
		playerHUD.setMP(playerUnit.currentMP);
<<<<<<< Updated upstream
		dialogueText.text = "You feel renewed strength!";
=======
		dialogueText.text = "Силы восстанавливаются!";
>>>>>>> Stashed changes

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYER_CHOOSED_ACTION;
		StartCoroutine(EnemyTurn());
	}

	public IEnumerator PlayerMagic(string aliment)
	{
		float elem = 1f;
		int elemInd = 0;
		switch (aliment)
		{
			case "lightning":
				elem = 2f;
				elemInd = 1;
				break;
			case "fire":
				elem = 1.5f;
				elemInd = 2;
				break;
			case "water":
				elem = 0.5f;
				elemInd = 3;
				break;
		}
		
		playerGO.GetComponent<UnityArmatureComponent>().animation.Play("Scroll", 1);
		yield return new WaitForSeconds(1f);
		Instantiate(effects[elemInd], enemyBattleStation);
		var damage = Mathf.RoundToInt(playerUnit.damage * elem);
		var fullDamage = Mathf.RoundToInt(UnityEngine.Random.Range(damage * 0.5f, damage * 1.5f));
		bool isDead = enemyUnit.TakeDamage(fullDamage);
		ChangeDialogueState();
		enemyHUD.SetHP(enemyUnit.currentHP);
		playerHUD.setMP(playerUnit.currentMP);
		dialogueText.text = fullDamage +" урона!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.PLAYER_CHOOSED_ACTION;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator PlayerWait()
	{
		bool isDead = enemyUnit.TakeDamage(0);
		ChangeDialogueState();
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "Ждём...";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.PLAYER_CHOOSED_ACTION;
			StartCoroutine(EnemyTurn());
		}
	}
	
	public void OnAttackButton()
	{
		Debug.Log("Attack enemy");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		
		StartCoroutine(PlayerAttack());
	}
	
	public void OnSuperButton()
	{
		Debug.Log("Super enemy");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		
		StartCoroutine(PlayerShadowAttack());
	}

	public void OnMagicButton(string aliment)
	{
		if (!playerUnit.CanSpendMana())
			return;
		playerUnit.SpendMana();
		Debug.Log("Attack enemy");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		
		StartCoroutine(PlayerMagic(aliment));
	}

	public void OnHealButton()
	{
		Debug.Log("Heal yourself");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		playerGO.GetComponent<UnityArmatureComponent>().animation.Play("DrinkHP", 1);
		StartCoroutine(PlayerRestore(true, false));
	}
	
	public void OnManaButton()
	{
		Debug.Log("Restore yourself");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		playerGO.GetComponent<UnityArmatureComponent>().animation.Play("DrinkMana", 1);
		StartCoroutine(PlayerRestore(false, true));
	}
	
	public void OnBeerButton()
	{
		Debug.Log("Heal and restore yourself");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		playerGO.GetComponent<UnityArmatureComponent>().animation.Play("dRINNKbeer", 1);
		StartCoroutine(PlayerRestore(true, true));
	}

	public void OnWaitButton()
	{
		Debug.Log("Wait one round!");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;

		StartCoroutine(PlayerWait());
	}

	public void ReturnToBasePanel()
	{
		Panels[0].GetComponent<CanvasGroup>().alpha = 1;
		Panels[0].SetActive(true);
				
		Panels[PanelIndex].GetComponent<CanvasGroup>().alpha = 0;
		Panels[PanelIndex].SetActive(false);
		PanelIndex = 0;
	}
	public void ChangePanels(int newIndex)
	{
		Panels[newIndex].GetComponent<CanvasGroup>().alpha = 1;
		Panels[newIndex].SetActive(true);
			
		Panels[PanelIndex].GetComponent<CanvasGroup>().alpha = 0;
		Panels[PanelIndex].SetActive(false);
		PanelIndex = newIndex;
	}
	
	public void ChangeDialogueState()
	{
		if (dialogueSwitch)
		{
			dialoguePanel.GetComponent<CanvasGroup>().alpha = 0;
			dialoguePanel.SetActive(false);
			dialogueSwitch = false;
		}
		else
		{
			dialoguePanel.GetComponent<CanvasGroup>().alpha = 1;
			dialoguePanel.SetActive(true);
			dialogueSwitch = true;
		}
	}
}
