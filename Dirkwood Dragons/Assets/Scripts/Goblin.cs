using UnityEngine;
using System.Collections;

public class Goblin : MovingObject {

	public int playerDamage;			// Determine how much damage to cause the player when attacking


	private Animator anim;
	private Transform target;
	private bool skipMove;

	// Overriding the function in our base class
	protected override void Start () {
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void AttemptMove <T> (int xDir, int yDir) {
		if (skipMove) {
			skipMove = false;
			return;
		}

		base.AttemptMove <T> (xDir, yDir);

		skipMove = true;
	}

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
		HeroAnimation hitPlayer = component as HeroAnimation;

		hitPlayer.LoseHealth (playerDamage);
	}
}
