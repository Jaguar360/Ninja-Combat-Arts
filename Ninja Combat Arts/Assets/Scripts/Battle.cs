using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Battle : MonoBehaviour 
{
	public Text text;
	public Text healthtext;
	public Button attack;
	public Button pyro;
	public Button recov;
	public Button sushi;
	public Enemy[] enemies;

	private Enemy curEnemy;
	private int enCount;
    private bool gameOver;
    private int sceneNum;
	private Player ninja;
	private int i;
	private Recovpo recovpo;
	private DamageNinpo dmgSpell;
	private HealItem healItem;

	// Use this for initialization
	void Start () 
	{
		attack.gameObject.SetActive(false);
        pyro.gameObject.SetActive(false);
        recov.gameObject.SetActive(false);
        sushi.gameObject.SetActive(false);

		healthtext.gameObject.SetActive(false);

		enCount = enemies.Length;
		gameOver = false;
        sceneNum = SceneManager.GetActiveScene().buildIndex;
		i = 0;
		recovpo = FindObjectOfType<Recovpo>();
		dmgSpell = FindObjectOfType<DamageNinpo>();
		healItem = FindObjectOfType<HealItem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.R)) 
		{
			SceneManager.LoadScene(sceneNum);
		}


	}

	public void Begin(Player player) 
	{
		ninja = player;
		Debug.Log("Battle started.");

		attack.gameObject.SetActive(true);
		pyro.gameObject.SetActive(true);
		recov.gameObject.SetActive(true);
		sushi.gameObject.SetActive(true);

		healthtext.gameObject.SetActive(true);
        
		curEnemy = enemies[i];
		curEnemy.gameObject.SetActive(true);

		attack.onClick.AddListener(Attack);
		recov.onClick.AddListener(Recov);
		pyro.onClick.AddListener(Pyro);
		sushi.onClick.AddListener(Sushi);

		UpdateHealth();      

		//if (!player.IsAlive())
        //{
        //    gameOver = true;
        //}      
	} 

	public void Continue() 
	{
		if (i < enCount)
		{
			curEnemy = enemies[i];
			curEnemy.gameObject.SetActive(true);
		}

		else 
		{
			foreach (Button button in FindObjectsOfType<Button>())
            {
                button.gameObject.SetActive(false);
            }
            text.text = "Congratulations, you won!\nPress Esc or R to play again.";	
		}

		UpdateHealth();

		if (!ninja.IsAlive())
        {
            gameOver = true;
        }

		if (curEnemy == null)
        {
            foreach (Button button in FindObjectsOfType<Button>())
            {
                button.gameObject.SetActive(false);
            }
            text.text = "Congratulations, you won!\nPress Esc or R to play again.";
        }
	}

	void Attack() 
	{
		int damage = ninja.Attack();

		Debug.Log(curEnemy.name + " took " + damage + " damage!");
		text.text = curEnemy.name + " took " + damage + " damage!";

		int enemyDamage = curEnemy.Attack();

		Debug.Log(ninja.name + " took " + enemyDamage + " damage!");
		text.text += "\n" + ninja.name + " took " + enemyDamage + " damage!";

        curEnemy.TakeDamage(damage);
        
		if (!curEnemy.IsAlive())
        {
            i++;
			text.text += "\nThe enemy died.";
        }

		ninja.TakeDamage(enemyDamage);

		if (gameOver) 
        {
            foreach(Button button in FindObjectsOfType<Button>()) 
            {
                button.gameObject.SetActive(false);
            }
            text.text += "\nGame Over\nPress Esc or R to restart";
        }   
              
		UpdateHealth();
		Continue();
	}

	void Pyro() 
	{
		if (ninja.getCurKi() < dmgSpell.ki.getBase())
		{
			text.text = "Not enough Ki for this technique";
		}

		else
		{
			int damage = dmgSpell.pow.getBase() * ninja.kipo.getBase() / 20 + (int)Random.Range(-2, 4);
			ninja.consumeKi(dmgSpell.ki.getBase());

			Debug.Log(curEnemy.name + " took " + damage + " damage!");
			text.text = curEnemy.name + " took " + damage + " damage!";

			int enemyDamage = curEnemy.Attack();

			Debug.Log(ninja.name + " took " + enemyDamage + " damage!");
			text.text += "\n" + ninja.name + " took " + enemyDamage + " damage!";

			curEnemy.TakeDamage(damage);

			if (!curEnemy.IsAlive())
			{
				i++;
				text.text += "\nThe enemy died.";
			}

			ninja.TakeDamage(enemyDamage);

			if (gameOver)
			{
				foreach (Button button in FindObjectsOfType<Button>())
				{
					button.gameObject.SetActive(false);
				}
				text.text += "\nGame Over\nPress Esc or R to restart";
			}
		}

        UpdateHealth();
        Continue();
	}

	void Recov() 
	{
		if (ninja.getCurKi() < recovpo.ki.getBase()) 
		{
			text.text = "Not enough Ki for this technique";
		}

		else
		{
			int healAmount = recovpo.baseHeal.getBase() * ninja.kipo.getBase() / 20;
			ninja.Heal(healAmount);

			ninja.consumeKi(recovpo.ki.getBase());

			Debug.Log(ninja.name + " healed " + healAmount + " damage!");
			text.text = "\n" + ninja.name + " healed " + healAmount + " damage!";

			int enemyDamage = curEnemy.Attack();

            Debug.Log(ninja.name + " took " + enemyDamage + " damage!");
            text.text += "\n" + ninja.name + " took " + enemyDamage + " damage!";

			ninja.TakeDamage(enemyDamage);

            if (gameOver)
            {
                foreach (Button button in FindObjectsOfType<Button>())
                {
                    button.gameObject.SetActive(false);
                }
                text.text += "\nGame Over\nPress Esc or R to restart";
            }

			UpdateHealth();
			Continue();
		}
	}

	void Sushi() 
	{
		if (healItem.count < 1) 
		{
			text.text = "Ran out of " + healItem.name;	
		}

		else 
		{
			ninja.Heal(healItem.healAmt);
			healItem.count--;

			Debug.Log(ninja.name + " healed " + healItem.healAmt + " damage!");
			text.text = "\n" + ninja.name + " healed " + healItem.healAmt + " damage!";

			int enemyDamage = curEnemy.Attack();

            Debug.Log(ninja.name + " took " + enemyDamage + " damage!");
            text.text += "\n" + ninja.name + " took " + enemyDamage + " damage!";

			ninja.TakeDamage(enemyDamage);

            if (gameOver)
            {
                foreach (Button button in FindObjectsOfType<Button>())
                {
                    button.gameObject.SetActive(false);
                }
                text.text += "\nGame Over\nPress Esc or R to restart";
            }

            UpdateHealth();
			Continue();
		}
	}

	void UpdateHealth() 
	{
		healthtext.text = "HP = " + ninja.getCurHP() + " | Ki = " + ninja.getCurKi();	
	}
}
