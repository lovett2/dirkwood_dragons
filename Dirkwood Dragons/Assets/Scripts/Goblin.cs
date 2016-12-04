using UnityEngine;
using System.Collections;

public class Goblin : MovingObject {
	
	[HideInInspector] public bool facingRight = true;
	public int playerDamage;			// Determine how much damage to cause the player when attacking
	public float moveForce = 10f;
	public float maxSpeed = 5f;
	public float maxDist = 10;
	public float minDist = 5;


	private Animator anim;				//Variable of type Animator to store a reference to the enemy's Animator component.
	private Transform target;			//Transform to attempt to move toward each turn.
	private bool skipMove;				//Boolean to determine whether or not enemy should skip a turn or move this turn.

	// Overriding the function in our base class
	protected override void Start () {
		//Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
		//This allows the GameManager to issue movement commands
		//GameManager.instance.AddGoblinToList (this);

		//Get and store a reference to the attached Animator component.
		anim = GetComponent<Animator> ();

		//Find the Player GameObject using it's tag and store a reference to its transform component.
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		//Call the start function of our base class MovingObject.
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = target.position - transform.position;
		int xDir = 0;
		int yDir = 0;

		// If the difference between the enemy and the player is less than or equal to the minimum distance, then the enemy moves towards the player
		if (Vector3.Distance (transform.position, target.position) <= minDist) {
			MoveEnemy();
		} else {
			// The enemy should walk back and forth across the screen whenever the player isn't in range
		}
		xDir = target.position.x > transform.position.x ? 1 : -1;
		AttemptMove <HeroAnimation> (xDir, yDir);
		MoveEnemy();

		if (direction.x < 0 && facingRight)
			Flip ();
		else if (direction.x > 0 && !facingRight)
			Flip();
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	protected override void AttemptMove <T> (int xDir, int yDir) {
		anim.SetTrigger ("goblinWalk");
		base.AttemptMove <T> (xDir, yDir);
	}

	//MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
	public void MoveEnemy() {
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon) {
			yDir = target.position.y > transform.position.y ? 1 : -1;
		} else {
			xDir = target.position.x > transform.position.x ? 1 : -1;
		}

		AttemptMove <HeroAnimation> (xDir, yDir);
	}

	protected override void OnCantMove <T> (T component) {
		if (component.gameObject.tag == "Wall") {
			// Stop and move in the other direction

			Flip();

		} else if (component.gameObject.tag == "Player") {
			//Declare hitPlayer and set it to equal the encountered component.
			HeroAnimation hitPlayer = component as HeroAnimation;

			//Set the attack trigger of animator to trigger Enemy attack animation.
			// anim.SetTrigger("goblinAttack");

			//Call the LoseHealth function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
			hitPlayer.LoseHealth (playerDamage);
		}
	}
		
}
