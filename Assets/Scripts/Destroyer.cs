using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerExit2D(Collider2D col)
	{
//		if(col.tag == "Column" || col.tag == "Floor")
		if (col.tag == "Player")
			GameManager.instance.GameOver ();

		Destroy (col.gameObject);
	}
}
