﻿using UnityEngine;
using System.Collections;

public abstract class MovingObject: MonoBehaviour {

	public float moveTime = 0.1f;          //Time it will take object to move, in seconds.
	public LayerMask blockingLayer;        //Layer on which collision will be checked.

	private BoxCollider2D boxCollider;     //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2d;              //The Rigidbody2D component attached to this object.
	private float inverseMoveTime;         //Used to make movement more efficient.

	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;
	}

	protected bool Move(int xDir, int yDir, out RaycastHit2D hit) {
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;

		if (hit.transform == null) {
			StartCoroutine (SmoothMovement (end));
			return true;
		}

		return false;
	}

	protected virtual void AttemptMove <T> (int xDir, int yDir) 
		where T: Component
	{
		RaycastHit2D hit;
		bool canMove = Move (xDir, yDir, out hit);

		if (hit.transform == null)
			return;

		T hitComponent = hit.transform.GetComponent<T> ();

		if (!canMove && hitComponent != null) {
			OnCantMove (hitComponent);
		}
	}

	protected IEnumerator SmoothMovement (Vector3 end) {
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (rb2d.position, end, inverseMoveTime * Time.deltaTime);
			rb2d.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected abstract void OnCantMove <T> (T component) 
		where T: Component;
}
