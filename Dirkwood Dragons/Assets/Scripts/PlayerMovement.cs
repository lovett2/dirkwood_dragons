using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	private Rigidbody2D rb2d;

	private int jumpTimer = 0;
	private int maxJumpTime = 50;
	private int jumpLaunch = 0;
	private int maxJumpLaunch = 1;

	bool grounded = false;
	public Transform groundCheck;
	float groundLength = 0.2f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	/*
	void OnCollisionEnter(Collider col)
	{
		Debug.Log ("Collision Detected!");
		if (col.gameObject.name == "ground") {
			jumpTimer = 0;
			jumpLaunch = 0;
		}
	}
	*/
	void FixedUpdate() {
		grounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundLength, groundLength), whatIsGround);

		float h = Input.GetAxis ("Horizontal");
		rb2d.velocity = new Vector2 (h, 0);

		if (Input.GetKeyUp(KeyCode.Space))
			jumpLaunch++;

		if (Input.GetKey (KeyCode.Space) && jumpTimer < maxJumpTime && jumpLaunch < maxJumpLaunch) {
			rb2d.velocity = new Vector2 (h, 1);// (Vector2.up * 4);
			jumpTimer++;
			Debug.Log (jumpTimer);
		}
	}
}
