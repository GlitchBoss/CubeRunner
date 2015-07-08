using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Column : MonoBehaviour {

	public float tiltSpeed;
	public float targetAngle;
	public float yPosition;
	public PhysicsMaterial2D normalMat;
	public PhysicsMaterial2D stickyMat;
	public GameObject playerChild;
	public Transform player;
	public float force;

	bool hit;
	bool playerOn;

	void Start()
	{
		normalMat = GetComponent<BoxCollider2D> ().sharedMaterial;
		playerChild = transform.FindChild ("ColPlayer").gameObject;
	}

	void Update() {
		if (hit) {
			Quaternion q = Quaternion.AngleAxis(targetAngle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * tiltSpeed);
			if(CrossPlatformInputManager.GetButtonDown ("Jump") && playerOn)
			{
				GameObject p = Instantiate (player.gameObject, playerChild.transform.position,
				                            playerChild.transform.rotation) as GameObject;

				p.name = "Player";
				p.GetComponent<Rigidbody2D>().AddForce (p.transform.right * force, ForceMode2D.Impulse);
				p.transform.rotation = Quaternion.identity;
				playerChild.SetActive (false);
				playerOn = false;
				SoundManager.instance.PlaySFX (p.GetComponent<AudioSource>());
				return;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player"){
			
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player") {
//			if(col.gameObject.GetComponent<Collider2D>().bounds.min.y >= 
//			GetComponent<Collider2D>().bounds.max.y){
			if(col.transform.position.y >= yPosition)
			{
				if(!hit){
					hit = true;
					playerOn = true;
					Destroy (col.gameObject);
					playerChild.SetActive (true);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
