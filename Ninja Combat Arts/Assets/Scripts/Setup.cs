using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup : MonoBehaviour 
{
	public Text text;
	public Button Melee;
	public Button Ranged;
	public Button Mystic;
	public Button Android;
	public Button Human;
	public Button Kiman;

	private Player player;
	private int phase;
	private Battle battle;

	// Use this for initialization
	void Start () 
	{
		text.text = "Select your character!\n" +
			"Choose to be a:\n- Melee fighter\n" +
			"- Ranged fighter\n" +
			"- Mystic fighter";
		
		Melee.gameObject.SetActive(true);
		Ranged.gameObject.SetActive(true);
		Mystic.gameObject.SetActive(true);
		Human.gameObject.SetActive(false);
		Android.gameObject.SetActive(false);
		Kiman.gameObject.SetActive(false);

		phase = 1;
		battle = FindObjectOfType<Battle>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (phase == 1)
		{
			Melee.onClick.AddListener(MeleeSelect);
			Ranged.onClick.AddListener(RangedSelect);
			Mystic.onClick.AddListener(MysticSelect);
		}

		if (phase == 2)
        {
			text.text = "Select your race!\n" +
            "Choose to be a:\n- Human\n" +
            "- Android\n" +
            "- Kiman";

			Human.onClick.AddListener(HumanSelect);
			Android.onClick.AddListener(AndroidSelect);
			Kiman.onClick.AddListener(KimanSelect);
        }

		if (phase == 3) 
		{
			text.text = "Battle begin!";
			battle.Begin(player);
			phase++;
		}
	}

	void MeleeSelect()
	{
		foreach(Player ninja in FindObjectsOfType<Player>()) 
		{
			if (ninja.arch != "Melee")
			{
				ninja.gameObject.SetActive(false);
			}
		}

		Melee.gameObject.SetActive(false);
        Ranged.gameObject.SetActive(false);
        Mystic.gameObject.SetActive(false);
        Human.gameObject.SetActive(true);
        Android.gameObject.SetActive(true);
        Kiman.gameObject.SetActive(true);
		phase = 2;

	}

	void RangedSelect() 
	{
		foreach (Player ninja in FindObjectsOfType<Player>())
        {
            if (ninja.arch != "Ranged")
            {
                ninja.gameObject.SetActive(false);
            }
        }

		Melee.gameObject.SetActive(false);
        Ranged.gameObject.SetActive(false);
        Mystic.gameObject.SetActive(false);
        Human.gameObject.SetActive(true);
        Android.gameObject.SetActive(true);
        Kiman.gameObject.SetActive(true);
		phase = 2;
	}

	void MysticSelect() 
	{
		foreach (Player ninja in FindObjectsOfType<Player>())
        {
            if (ninja.arch != "Mystic")
            {
                ninja.gameObject.SetActive(false);
            }
        }

		Melee.gameObject.SetActive(false);
        Ranged.gameObject.SetActive(false);
        Mystic.gameObject.SetActive(false);
        Human.gameObject.SetActive(true);
        Android.gameObject.SetActive(true);
        Kiman.gameObject.SetActive(true);
		phase = 2;
	}

	void HumanSelect() 
	{
		foreach (Player ninja in FindObjectsOfType<Player>())
        {
			if (ninja.race != "Human")
            {
                ninja.gameObject.SetActive(false);
            }

			else if (ninja.race == "Human") 
			{
				player = ninja;
			}
        }

		Human.gameObject.SetActive(false);
        Android.gameObject.SetActive(false);
        Kiman.gameObject.SetActive(false);
		phase = 3;

		Debug.Log(player.name);
	}

	void AndroidSelect()
    {
		foreach (Player ninja in FindObjectsOfType<Player>())
        {
            if (ninja.race != "Android")
            {
                ninja.gameObject.SetActive(false);
            }

            else if (ninja.race == "Android") 
            {
                player = ninja;
            }
        }   

		Human.gameObject.SetActive(false);
        Android.gameObject.SetActive(false);
        Kiman.gameObject.SetActive(false);
        phase = 3;

		Debug.Log(player.name);
    }

	void KimanSelect()
    {
		foreach (Player ninja in FindObjectsOfType<Player>())
        {
            if (ninja.race != "Kiman")
            {
                ninja.gameObject.SetActive(false);
            }

            else if (ninja.race == "Kiman")
            {
                player = ninja;
            }
        }

		Human.gameObject.SetActive(false);
        Android.gameObject.SetActive(false);
        Kiman.gameObject.SetActive(false);
        phase = 3;

        Debug.Log(player.name);
    }
}
