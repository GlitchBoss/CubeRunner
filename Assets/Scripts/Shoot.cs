using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public float speed;

	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			GetComponent<Rigidbody2D>().AddForce ((transform.up) * speed,
			                                      ForceMode2D.Impulse);
		}
		transform.Rotate (Vector3.forward, 2);
	}
}
