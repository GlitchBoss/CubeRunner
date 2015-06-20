using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour {

	public float tiltSpeed;
	public float targetAngle;
	public float yPosition;
	public PhysicsMaterial2D normalMat;
	public PhysicsMaterial2D stickyMat;


//	Player playerScript;
	bool hit;

	void Update() {
		if (hit) {
			Quaternion q = Quaternion.AngleAxis(targetAngle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * tiltSpeed);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player"){
			Player playerScript = col.GetComponent<Player>();
			if(col.transform.parent == null && playerScript.numOfColliders <= 1)
			{
				playerScript.numOfColliders++;
	//			playerScript.stuckOffset = col.transform.position - this.transform.position;
				playerScript.stuck = true;
				playerScript.objStuckTo = this.transform;
//				col.rigidbody.velocity = Vector2.zero;
//				col.rigidbody.isKinematic = true;
				col.attachedRigidbody.constraints = RigidbodyConstraints2D.None;
				col.attachedRigidbody.gravityScale = 0;
				col.transform.SetParent (this.transform);
				GetComponent<BoxCollider2D>().sharedMaterial = stickyMat;
			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player") {
//			if(col.gameObject.GetComponent<Collider2D>().bounds.min.y >= GetComponent<Collider2D>().bounds.max.y)
				if(!hit)
					hit = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			Player playerScript = col.GetComponent<Player>();
//			playerScript.numOfColliders--;
			playerScript.stuck = false;
			playerScript.objStuckTo = col.transform;
//			col.rigidbody.isKinematic = false;
			col.attachedRigidbody.gravityScale = 11;
			col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
			col.transform.rotation = Quaternion.identity;
			col.transform.parent = null;
			col.transform.localScale = new Vector3(1, 1, 1);
			GetComponent<BoxCollider2D>().sharedMaterial = normalMat;
		}
	}
}
