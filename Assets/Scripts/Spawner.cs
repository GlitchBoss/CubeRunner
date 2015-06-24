using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Analytics;

public class Spawner : MonoBehaviour {

	public float waitTime;
	public GameObject[] columns;

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
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Floor") {
			Debug.Log ("Instantiating...");
			if(previousColumn == 3)
			{
				Instantiate (columns[2], 
				             new Vector3(transform.position.x, transform.position.y, 0.0f),
				             columns[0].transform.rotation);
				previousColumn--;
				return;
			}

			int index = Random.Range (0, 3);
			
			switch (index) {
				
			case 1:
				
				if (previousColumn < columns.Length) {
					Instantiate (columns [previousColumn + 1],
					             new Vector3(transform.position.x, transform.position.y, 0.0f),
					             columns[0].transform.rotation);
					previousColumn++;
					Debug.Log ("Up one");
				}
				break;
				
			case 2:
				if (previousColumn > 0) {
					Instantiate (columns[previousColumn - 1], 
					             new Vector3(transform.position.x, transform.position.y, 0.0f), 
					             columns[0].transform.rotation);
					previousColumn--;
					Debug.Log ("Down one");
				}

				else{
					Instantiate (columns [previousColumn],
					             new Vector3 (transform.position.x, transform.position.y, 0.0f),
					             columns [0].transform.rotation);
					Debug.Log ("Same");
				}
				break;
				
			default:
				Instantiate (columns [previousColumn],
				             new Vector3 (transform.position.x, transform.position.y, 0.0f),
				             columns [0].transform.rotation);
				Debug.Log ("Same");
				break;
			}

			numOfCols--;
		}
	}
}




