using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_right"])) {
			transform.position = new Vector2 (transform.position.x + 3f * Time.fixedDeltaTime, transform.position.y);
		} else if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_left"])) {
			transform.position = new Vector2 (transform.position.x - 3f * Time.fixedDeltaTime, transform.position.y);
		} else if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_fire"])) {
			Debug.Log ("Fire");
		}
	}
}
