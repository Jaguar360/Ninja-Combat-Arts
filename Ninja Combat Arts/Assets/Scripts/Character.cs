using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public new string name;
	public Stat hp;
	public Stat pow;
	public Stat acc;
	private int curHP;
	private bool isAlive;

	void Start()
	{
		curHP = hp.getBase();
		isAlive = true;
	}

	public int Attack()
	{
		int dmg = pow.getBase() / 2 + (int)Random.Range(-2, 4);
		if (dmg <= 0)
		{
			dmg = 0;
		}
		return dmg;
	}

	public void TakeDamage(int damage)
	{
		curHP -= damage;
		if (curHP <= 0) 
		{
			Die();
		}
	}

	public void Heal(int amount) 
	{
		curHP += amount;
		if (curHP >= hp.getBase()) 
		{
			curHP = hp.getBase();
		}
	}

	public void Dead() 
	{
		isAlive = false;
	}

	public bool IsAlive() 
	{
		return isAlive;
	}

	public virtual void Die()
	{
		Dead();
		Debug.Log(name + "has died.");

	}

	public int getCurHP()
	{
		return curHP;
	}
}
