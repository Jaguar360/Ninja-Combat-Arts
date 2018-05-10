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
    private bool gameOver;
    private int sceneNum;
	private Player ninja;
	private int i;
	// Use this for initialization
	void Start () 
	{
		attack.gameObject.SetActive(false);
        pyro.gameObject.SetActive(false);
        recov.gameObject.SetActive(false);
        sushi.gameObject.SetActive(false);

		healthtext.gameObject.SetActive(false);
  
		gameOver = false;
        sceneNum = SceneManager.GetActiveScene().buildIndex;
		i = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameOver) 
		{
			foreach(Button button in FindObjectsOfType<Button>()) {
				button.gameObject.SetActive(false);
			}
			text.text = "Game Over\nPress Esc or R to restart";
		}	

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

		if (!curEnemy.IsAlive())
        {
            i++;
        }

		//if (!player.IsAlive())
        //{
        //    gameOver = true;
        //}

	} 

	void Attack() 
	{
		curEnemy.TakeDamage(ninja.Attack());
		text.text += "\n" + curEnemy.name + "took " + ninja.Attack() + "damage!";
	}

	void Pyro() {
		
	}

	void Recov() {
		
	}

	void Sushi() {
		
	}

	void UpdateHealth() 
	{
		healthtext.text = "HP = " + ninja.getCurHP() + " | Ki = " + ninja.getCurKi();	
	}
}
