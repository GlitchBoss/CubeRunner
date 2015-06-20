using UnityEngine;
using System.Collections;
//using UnityEngine.Analytics;
//using System.Collections.Generic;

public class Finish : MonoBehaviour {

	int totalPotions = 5;
	int totalCoins = 100;
	string weaponID = "Weapon_102";

	Spawner spawner;

	void Start()
	{
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			//Analytics.CustomEvent ("gameOver", new Dictionary<string, object>{
			//	{ "lastColumn", spawner.previousColumn }
			//});
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
