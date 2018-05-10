using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
	public Stat ki;
	public Stat kipo;
	public string race;
	public string arch;
	private int curKi;

	void Awake()
	{
		curKi = ki.getBase();
	}
    
	public int getCurKi()
    {
        return curKi;
    }
}
