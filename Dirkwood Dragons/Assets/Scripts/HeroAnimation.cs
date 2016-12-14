using UnityEngine;
using System.Collections;

public class HeroAnimation : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 10f;
	public float maxSpeed = 5f;
	public float jumpForce = 15f;
	public Transform groundCheck;
	public int numJumps = 2;
	public int maxNumJumps = 2;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector2 FindGround = transform.position - Vector2.down;
		if(Physics.Linecast (transform.position,  FindGround)){
			Debug.Log ("On Ground");
		}*/
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("speed", Mathf.Abs (h));
		//Debug.Log (Input.GetAxis ("Horizontal"));

		if (Input.GetButtonDown ("Jump") ) {
			jump = true;
		}
			
		rb2d.AddForce(new Vector2 (h, 0));
//		if (h * rb2d.velocity.x < maxSpeed)
//			rb2d.AddForce (Vector2.right * h * moveForce);
//		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
//			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip();

		if (jump && numJumps > 0) {
			anim.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0, jumpForce));
			jump = false;
			numJumps--;
		}

		//rb2d.AddForce(new Vector2(h, v));
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void LoseHealth (int loss) {
		//To be implemented: allows player to take loss when attacked by enemy (goblin)

		Debug.Log ("Losing Health!");

		//Set the trigger for the player animator to transition to the playerHit animation.
		//anim.SetTrigger ("playerHit");

		//Subtract lost food points from the players total.
		//health -= loss;

		//Check to see if game has ended.
		//CheckIfGameOver();
	}

	//protected override void OnCantMove <T> (T component) {
		//To be implemented: allows player to break through walls

		//Set hitWall to equal the component passed in as a parameter.
		//Wall hitWall = component as Wall;

		//Call the DamageWall function of the Wall we are hitting.
		//hitWall.DamageWall (wallDamage);

		//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
		//animator.SetTrigger ("playerChop");
	//}
}
