using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Analytics;

public class Spawner : MonoBehaviour {

	public float waitTime;
	public GameObject[] columns;
	public float offset = 0.1f;

	public int previousColumn;

	int numOfCols;

	void Start()
	{
//		Instantiate (columns [0],
//	             new Vector3 (transform.position.x, transform.position.y, 0.0f), 
//	             columns [0].transform.rotation);
		previousColumn = 0;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Floor") {
			numOfCols++;
//			if(numOfCols >= 3)
//			{
//				if(col.transform.parent != null) {
//					Destroy(col.transform.parent.gameObject);
//				}
//				else {
//					Destroy(col.gameObject);
//				}
//			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Floor") {
			if(numOfCols >= 3)
			{
				if(col.transform.parent != null) {
					Destroy(col.transform.parent.gameObject);
				}
				else {
					Destroy(col.gameObject);
				}
			}
			if(numOfCols <= 0)
			{
				Spawn (previousColumn);
				Debug.Log ("Saved Spawn! :)");
			}
		}
	}

	void LateUpdate()
	{
		if(numOfCols <= 0)
		{
			Spawn (previousColumn);
			Debug.Log ("Saved Spawn! :)");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Floor") {
			Debug.Log ("Instantiating...");
			if(previousColumn == 3)
			{
//				Instantiate (columns[2], 
//				             new Vector3(transform.position.x - offset, transform.position.y, 0.0f),
//				             columns[0].transform.rotation);
				Spawn (2);
				previousColumn--;
				return;
			}

			int index = Random.Range (0, 3);
			
			switch (index) {
				
			case 1:
				
				if (previousColumn < columns.Length) {
//					Instantiate (columns [previousColumn + 1],
//					             new Vector3(transform.position.x - offset, transform.position.y, 0.0f),
//					             columns[0].transform.rotation);
					Spawn (previousColumn + 1);
					previousColumn++;
					Debug.Log ("Up one");
				}
				break;
				
			case 2:
				if (previousColumn > 0) {
//					Instantiate (columns[previousColumn - 1], 
//					             new Vector3(transform.position.x - offset, transform.position.y, 0.0f), 
//					             columns[0].transform.rotation);
					Spawn (previousColumn - 1);
					previousColumn--;
					Debug.Log ("Down one");
				}

				else{
//					Instantiate (columns [previousColumn],
//					             new Vector3 (transform.position.x - offset, transform.position.y, 0.0f),
//					             columns [0].transform.rotation);
					Spawn (previousColumn);
					Debug.Log ("Same");
				}
				break;
				
			default:
//				Instantiate (columns [previousColumn],
//				             new Vector3 (transform.position.x, transform.position.y, 0.0f),
//				             columns [0].transform.rotation);
				Spawn (previousColumn);
				Debug.Log ("Same");
				break;
			}

			numOfCols--;
		}
	}

	void Spawn(int index)
	{
		Instantiate (columns [index],
		             new Vector3 (transform.position.x, transform.position.y, 0.0f),
		             columns [0].transform.rotation);
	}
}




