using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
	public Rigidbody2D rb;
	public int	jump = 300;
	public int	move = 100;
	bool is_jump;
	float x;
	float y;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 ((AreaLimits.LeftLimit () + AreaLimits.RightLimit ()) / 2, +AreaLimits.UpLimit(), 0);
		rb = GetComponent<Rigidbody2D>();

		is_jump = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_right"]) || Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_left"]) || (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_jump"]) && is_jump == false)) {
			y = rb.velocity.y;
			x = rb.velocity.x;
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_right"])) {
				x = move * Time.fixedDeltaTime;
			}
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_left"])) {
				x = -move * Time.fixedDeltaTime;
			}
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_jump"]) && is_jump == false) {
				y = jump * Time.fixedDeltaTime;
				is_jump = true;
			}
			rb.velocity = new Vector2 (x, y);
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		is_jump = false;
	}
}
