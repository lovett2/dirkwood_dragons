using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("speed", Mathf.Abs (h));

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip();
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
