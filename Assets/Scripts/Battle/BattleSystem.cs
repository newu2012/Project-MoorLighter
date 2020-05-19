using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYER_CHOOSED_ACTION, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	BattleUnit playerUnit;
	BattleUnit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	public Image dialogueImage;
	public bool dialogueSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		Debug.Log("Setup Battle");
		
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<BattleUnit>();
		playerGO.transform.localScale = new Vector3(0.5f, 0.5f, 0);
		playerGO.transform.position = new Vector3(-6, -0.5f, 0);
		
		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<BattleUnit>();
		enemyGO.transform.localScale = new Vector3(0.25f, 0.25f, 0);
		enemyGO.transform.position = new Vector3(6, -1f, 0);
		
		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);
		
		yield return new WaitForSeconds(2f);
		Debug.Log("Before Battle");
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		ChangeDialogueState();
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

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

	IEnumerator EnemyTurn()
	{
		state = BattleState.ENEMYTURN;
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

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
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		Debug.Log("Player Turn");
		dialogueText.text = "Choose an action:";
		ChangeDialogueState();
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);
		ChangeDialogueState();
		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYER_CHOOSED_ACTION;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		Debug.Log("Attack enemy");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;
		
		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		Debug.Log("Heal yourself");
		if (state != BattleState.PLAYERTURN && state != BattleState.PLAYER_CHOOSED_ACTION)
			return;

		StartCoroutine(PlayerHeal());
	}

	public void ChangeDialogueState()
	{
		if (dialogueSwitch)
		{
			var color = dialogueImage.color;
			color = new Color(color.r, color.g, color.b, 0f );
			dialogueImage.color = color;
			dialogueText.text = "";
			dialogueSwitch = false;
			dialogueImage.gameObject.SetActive(false);
			dialogueText.gameObject.SetActive(false);
			return;
		}
		else
		{
			var color = dialogueImage.color;
			color = new Color(color.r, color.g, color.b, 1f );
			dialogueImage.color = color;
			dialogueText.text = "";
			dialogueSwitch = true;
			dialogueImage.enabled = dialogueSwitch;
			dialogueText.enabled = dialogueSwitch;
			dialogueImage.gameObject.SetActive(true);
			dialogueText.gameObject.SetActive(true);
			return;
		}
	}
}
