using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

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
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown ("Jump") && grounded) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0.0f, jumpForce), ForceMode2D.Impulse);
			grounded = false;
		}

		if (numOfColliders < 0)
			numOfColliders = 0;
		m_rigidbody.velocity = new Vector2 (speed, m_rigidbody.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag != "Column")
			numOfColliders++;
		if (col.gameObject.tag == "Floor")
			grounded = true;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		numOfColliders--;
		if (numOfColliders > 0)
			numOfColliders = 0;
		m_rigidbody.gravityScale = 11;
	}

	void FixedUpdate()
	{
		if(groundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers (whatIsGround)){
			grounded = true;
		}
	}
}
