using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset;

	void Start()
	{
		transform.position = new Vector3 (target.position.x + offset.x, 
		                                 transform.position.y, 
		                                 target.position.z + offset.z);
	}

	void FixedUpdate()
	{
		if (!target) {
			Application.LoadLevel (Application.loadedLevel);
			return;
		}

		transform.position = new Vector3 (target.position.x + offset.x, 
		                                  transform.position.y, 
		                                  target.position.z + offset.z);
	}
}
