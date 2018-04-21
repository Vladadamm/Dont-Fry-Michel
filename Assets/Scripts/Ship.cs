using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	public	float	size_sprite = 0.2f;
	public	float	acceleration = 5f;
	public float	max_speed = 5f;
	public float	fire_rate = 0.5f;
	public float	speed;
	float last_shot;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 ((AreaLimits.LeftLimit () + AreaLimits.RightLimit ()) / 2, AreaLimits.BottomLimit ()+0.5f, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_right"])) {
			if (speed < max_speed)
				speed += acceleration * Time.fixedDeltaTime;
		} else if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_left"])) {
			if (speed > -max_speed)
				speed -= acceleration * Time.fixedDeltaTime;
		} else {
			if (speed > 0)
				speed -= Time.fixedDeltaTime;
			else if (speed < 0)
				speed += Time.fixedDeltaTime;
		}
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_fire"]) && (Time.fixedTime > last_shot + fire_rate)) {
			GameObject bullet = (GameObject)GameObject.Instantiate (Resources.Load ("Bullet"));
			bullet.transform.position = transform.position;
			last_shot = Time.fixedTime;
		}
		if (transform.position.x + speed * Time.fixedDeltaTime > AreaLimits.LeftLimit() + size_sprite && transform.position.x + speed * Time.fixedDeltaTime < AreaLimits.RightLimit() - size_sprite)
			transform.position = new Vector3 (transform.position.x + speed * Time.fixedDeltaTime, transform.position.y);
		else
			speed = -(speed / 2);
	}
}
