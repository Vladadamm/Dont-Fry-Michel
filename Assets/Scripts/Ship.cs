using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public float fire_rate = 0.5f;
	float last_shot;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 ((AreaLimits.LeftLimit () + AreaLimits.RightLimit ()) / 2, AreaLimits.BottomLimit ()+0.5f, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_right"])) {
			transform.position = new Vector2 (transform.position.x + 3f * Time.fixedDeltaTime, transform.position.y);
		} else if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_left"])) {
			transform.position = new Vector2 (transform.position.x - 3f * Time.fixedDeltaTime, transform.position.y);
		} 
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_fire"]) && (Time.fixedTime > last_shot + fire_rate)) {
			GameObject bullet = (GameObject)GameObject.Instantiate (Resources.Load ("Bullet"));
			bullet.transform.position = transform.position;
			last_shot = Time.fixedTime;
		}
	}
}
