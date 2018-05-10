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

	void Awake()
	{
		curHP = hp.getBase();
		isAlive = true;
	}

	public int Attack()
	{
		int dmg = pow.getBase() / 2 + (int)Random.Range(-1, 2);
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

	public void Dead() 
	{
		isAlive = false;
	}

	public bool IsAlive() 
	{
		return isAlive;
	}

	public void Die()
	{
		Dead();
		Debug.Log(name + "has died.");

	}

	public int getCurHP()
	{
		return curHP;
	}
}
