using UnityEngine;
using System.Collections;

public class HeroAnimation : MovingObject {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 10f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	protected override void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		Debug.Log (grounded);

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
			
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("speed", Mathf.Abs (h));

		rb2d.velocity = new Vector2 (h, rb2d.velocity.y);
//		if (h * rb2d.velocity.x < maxSpeed)
//			rb2d.AddForce (Vector2.right * h * moveForce);
//		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
//			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip();

		if (jump) {
			anim.SetTrigger ("Jump");
			rb2d.AddForce (new Vector3 (h, jumpForce, 0f));
			jump = false;
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void LoseHealth (int loss) {
		//To be implemented: allows player to take loss when attacked by enemy (goblin)


		//Set the trigger for the player animator to transition to the playerHit animation.
		//anim.SetTrigger ("playerHit");

		//Subtract lost food points from the players total.
		//health -= loss;

		//Check to see if game has ended.
		//CheckIfGameOver();
	}

	protected override void OnCantMove <T> (T component) {
		//To be implemented: allows player to break through walls

		//Set hitWall to equal the component passed in as a parameter.
		//Wall hitWall = component as Wall;

		//Call the DamageWall function of the Wall we are hitting.
		//hitWall.DamageWall (wallDamage);

		//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
		//animator.SetTrigger ("playerChop");
	}
}
