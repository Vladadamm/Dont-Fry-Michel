using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public SpriteRenderer renderer;

	public	int		nb_sprite = 61;
	public	float	size_sprite = 0.2f;
	public	float	acceleration = 5f;
	public float	max_speed = 5f;
	public float	load_rate = 0.25f;
	public	int	max_bullet = 10;
	public int min_bullet = 3;
	float	speed;
	float last_load;
	Object [] sprites;

	public int nb_bullet;
	public int nbBulletThrown;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 ((AreaLimits.LeftLimit () + AreaLimits.RightLimit ()) / 2, AreaLimits.BottomLimit () + 1f, 0);
		enabled = false;

		renderer =  GetComponent<SpriteRenderer>();
		sprites = Resources.LoadAll ("pipette-sprites-02");

		nb_bullet = min_bullet;
		nbBulletThrown = 0;
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
			if (nb_bullet >= min_bullet || nbBulletThrown != 0 && nb_bullet > 0) {
				nbBulletThrown++;
				nb_bullet--;
				GameObject bullet = (GameObject)GameObject.Instantiate (Resources.Load ("Bullet"));
				bullet.transform.position = transform.position;
			}
		} else if (Time.fixedTime >= last_load + load_rate && nb_bullet < max_bullet) {
			nb_bullet += 1;
			last_load = Time.fixedTime;
		}
		if (Input.GetKeyUp (InputManager.getInstance ().keyBinds ["Ship_fire"]))
			nbBulletThrown = 0;
		renderer.sprite = (Sprite)sprites [nb_sprite * nb_bullet /max_bullet];
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Block")) {
			Camera.main.gameObject.GetComponent<GameLogicManager> ().EndGame (false);
		}
	}
}
