using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float speed;
	public float dampTime = 0.15f;
	public bool autoFindPlayer;
	public bool smooth;

	Vector3 velocity = Vector3.zero;

	float normSpeed;

	void Start()
	{
		if (target) {
			transform.position = new Vector3 (target.position.x + offset.x, 
		                                 transform.position.y, 
		                                 target.position.z + offset.z);
		}

		normSpeed = speed;
	}

	void FixedUpdate()
	{
		if (!target) {
			if(GameObject.Find ("Player") && autoFindPlayer){
				target = GameObject.Find ("Player").transform;
			}else{
				transform.Translate (new Vector3(speed, 0, 0));
				return;
			}
		}

		if (smooth) {
			if(transform.position != target.position + offset)
			{
				Vector3 destination = Vector3.SmoothDamp (transform.position,
				                                          target.position + offset,
				                                          ref velocity,
				                                          dampTime);
				transform.position = new Vector3(destination.x,
				                                 transform.position.y,
				                                 transform.position.z);
			}
		} else {
			transform.position = new Vector3 (target.position.x + offset.x, 
		                                  transform.position.y, 
		                                  target.position.z + offset.z);
		}

//		Vector3 point = Camera.main.WorldToViewportPoint(target.position);
//		Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
//		Vector3 destination = transform.position + delta;
//		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
//		transform.position = new Vector3 (transform.position.x + offset.x, 3.64f, -10);

//		transform.position -= (transform.position - target.position) * 0.25f * Time.deltaTime;
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
