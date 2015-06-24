using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float speed;

	float normSpeed;

	void Start()
	{
		transform.position = new Vector3 (target.position.x + offset.x, 
		                                 transform.position.y, 
		                                 target.position.z + offset.z);

		normSpeed = speed;
	}

	void FixedUpdate()
	{
		if (!target) {
			transform.Translate (new Vector3(speed, 0, 0));
			return;
		}

		transform.position = new Vector3 (target.position.x + offset.x, 
		                                  transform.position.y, 
		                                  target.position.z + offset.z);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
			speed = normSpeed + 0.05f;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
			speed = normSpeed;
	}
}
