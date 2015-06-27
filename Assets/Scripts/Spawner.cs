using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Analytics;

public class Spawner : MonoBehaviour {

	public float waitTime;
	public GameObject[] columns;
	public float offset = 0.1f;

	public int previousColumn;

	public int numOfCols;

	void Start()
	{
		previousColumn = 0;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Floor") {
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Floor") {
			if(numOfCols >= 2)
			{
				if(col.transform.parent != null) {
					Destroy(col.transform.parent.gameObject);
				}
				else {
					Destroy(col.gameObject);
				}
				numOfCols--;
			}
		}
	}

	void FixedUpdate()
	{
		if(numOfCols < 0)
		{
			Spawn (previousColumn);
			Debug.Log ("Saved Spawn! :)");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Floor") {
			Debug.Log ("Instantiating...");
			numOfCols--;
			if(previousColumn == 3)
			{
				Spawn (2);
				previousColumn--;
				return;
			}

			int index = Choose (new float[3]{45, 45, 10}); 
			switch(index) {
				
			case 0:
				
				if (previousColumn < columns.Length) {
					Spawn (previousColumn + 1);
					previousColumn++;
					Debug.Log ("Up one");
				}else{
					Spawn(previousColumn);
					Debug.Log ("Same");
				}
				break;
				
			case 1:
				if (previousColumn > 0) {
					Spawn (previousColumn - 1);
					previousColumn--;
					Debug.Log ("Down one");
				}

				else{
					Spawn (previousColumn);
					Debug.Log ("Same");
				}
				break;
				
			default:
				Spawn (previousColumn);
				Debug.Log ("Same");
				break;
			}

		}
	}

	void Spawn(int index)
	{
		Instantiate (columns [index],
		             new Vector3 (transform.position.x - offset, transform.position.y, 0.0f),
		             columns [0].transform.rotation);
		numOfCols++;
	}

	int Choose (float[] probs) {
		
		float total = 0;
		
		foreach (float elem in probs) {
			total += elem;
		}
		
		float randomPoint = Random.value * total;
		
		for (int i= 0; i < probs.Length; i++) {
			if (randomPoint < probs[i]) {
				return i;
			}
			else {
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}
}




