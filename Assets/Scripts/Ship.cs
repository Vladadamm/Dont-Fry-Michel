using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private SpriteRenderer spriteRenderer; 

	public	int		nb_sprite = 10;
	public	float	size_sprite = 0.2f;
	public	float	acceleration = 5f;
	public float	max_speed = 5f;
	public float	load_rate = 0.5f;
	public	int	max_bullet = 5;
	float	speed;
	float last_load;
	int nb_bullet;
	Object [] sprites;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		transform.position = new Vector3 ((AreaLimits.LeftLimit () + AreaLimits.RightLimit ()) / 2, AreaLimits.BottomLimit () + 1f, 0);
		enabled = false;
		nb_bullet = max_bullet;
		sprites = Resources.LoadAll ("pipette-sprites-01");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		InputM();
		if (transform.position.x + speed * Time.fixedDeltaTime > AreaLimits.LeftLimit() + size_sprite && transform.position.x + speed * Time.fixedDeltaTime < AreaLimits.RightLimit() - size_sprite)
			transform.position = new Vector3 (transform.position.x + speed * Time.fixedDeltaTime, transform.position.y);
		else
			speed = -(speed / 2);

	}

	void InputM(){
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_right"])) {
			if (speed < max_speed)
				speed += acceleration * Time.fixedDeltaTime;
		} else if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_left"])) {
			if (speed > -max_speed)
				speed -= acceleration * Time.fixedDeltaTime;
		} else {
			if (speed > 0)
				speed = Mathf.Max(0,speed-5*Time.fixedDeltaTime);
			else if (speed < 0)
				speed = Mathf.Min(0,speed+5*Time.fixedDeltaTime);
		}
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_fire"])) {
			if (nb_bullet > 0) {
				nb_bullet--;
				GameObject bullet = (GameObject)GameObject.Instantiate (Resources.Load ("Bullet"));
				bullet.transform.position = transform.position;
			}
		}else if (Time.fixedTime >= last_load + load_rate && nb_bullet < max_bullet) {
			nb_bullet += 1;
			last_load = Time.fixedTime;
		}
		spriteRenderer.sprite = (Sprite)sprites [nb_bullet / max_bullet * ];
		Debug.Log (0 + nb_bullet);

	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Block")) {
			Camera.main.gameObject.GetComponent<GameLogicManager> ().EndGame (false);
		}
	}
}
