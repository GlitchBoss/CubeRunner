using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
			Application.LoadLevel (Application.loadedLevel);
	}
}
