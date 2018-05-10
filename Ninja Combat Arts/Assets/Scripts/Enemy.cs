using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	void Die()
    {
		Dead();
        Debug.Log(name + "has died.");
		Destroy(gameObject);
    }
}
