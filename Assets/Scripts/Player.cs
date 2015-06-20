using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
//using UnityEngine.Analytics;

public class Player : MonoBehaviour {

	public float jumpForce;
	public float speed;
	public LayerMask whatIsGround;
	public bool stuck;
	public Transform objStuckTo;
	public int numOfColliders;

	Transform groundCheck;
	public Vector3 stuckOffset;

	public bool grounded;

	Rigidbody2D m_rigidbody;

	const float k_GroundedRadius = .25f;




	void Start()
	{
		groundCheck = transform.Find("GroundCheck");
		m_rigidbody = GetComponent<Rigidbody2D> ();
//		Time.timeScale = 0.5f;
//		Time.fixedDeltaTime = Time.timeScale * 0.02f;

//		Analytics.SetUserBirthYear (2000);
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown ("Jump") && grounded) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0.0f, jumpForce), ForceMode2D.Impulse);
			grounded = false;
		}
		if (stuck) {
//			transform.rotation = objStuckTo.rotation;
//			transform.parent = objStuckTo;

//			transform.position = objStuckTo.position + stuckOffset;
//			stuckOffset = transform.position - objStuckTo.position;
		}

		if (numOfColliders < 0)
			numOfColliders = 0;
//		if(!stuck)
			m_rigidbody.velocity = new Vector2 (speed, m_rigidbody.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag != "Column")
			numOfColliders++;
		if (col.gameObject.tag == "Floor")
			GameManager.instance.GameOver ();
	}

	void OnCollisionExit2D(Collision2D col)
	{
		numOfColliders--;
		if (numOfColliders > 0)
			numOfColliders = 0;
//		m_rigidbody.isKinematic = false;
		m_rigidbody.gravityScale = 11;
		m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void FixedUpdate()
	{

		if (groundCheck.GetComponent<Collider2D> ().IsTouchingLayers (whatIsGround)) {
			grounded = true;
		}
	}
}
