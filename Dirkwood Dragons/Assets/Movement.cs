using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public float height;
	private float screenMaxX = Screen.width - 50;
	private float screenMaxY = Screen.height - 50;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal") * speed;
		float jump = Input.GetAxis ("Jump") * height;
		Vector3 movement = new Vector3 (moveHorizontal, jump, 0.0f);
		rb.AddForce (movement);

		if (transform.position.x > 10 || transform.position.x < -10) {
			transform.position = new Vector3 (10, transform.position.y, transform.position.z);
		}
		if (transform.position.y > 4) {
			transform.position = new Vector3 (transform.position.x, 4, transform.position.z);
		}
	}
}
