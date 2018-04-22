using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {


	public	int		nb_sprite = 61;
	public	float	size_sprite = 0.2f;
	public	float	acceleration = 5f;
	public float	max_speed = 5f;
	public float	load_rate = 0.25f;
	public	int	max_bullet = 15;
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
		if (!isShootingCannon) {
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_right"])) {
				if (speed < max_speed)
					speed += acceleration * Time.fixedDeltaTime;
			} else if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_left"])) {
				if (speed > -max_speed)
					speed -= acceleration * Time.fixedDeltaTime;
			} else {
				if (speed > 0)
					speed = Mathf.Max (0, speed - 5 * Time.fixedDeltaTime);
				else if (speed < 0)
					speed = Mathf.Min (0, speed + 5 * Time.fixedDeltaTime);
			}
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Ship_fire"])) {
				if (nb_bullet >= min_bullet + min_bullet_add || nbBulletThrown != 0 && nb_bullet > 0) {
					nbBulletThrown++;
					nb_bullet--;
					GameObject bullet = (GameObject)GameObject.Instantiate (Resources.Load ("Bullet"));
					bullet.transform.position = transform.position + Vector3.up;
				}
			} else if (Time.fixedTime >= last_load + load_rate / load_rate_mult && nb_bullet < max_bullet) {
				nb_bullet += 1;
				last_load = Time.fixedTime;
			}
			if (Input.GetKeyUp (InputManager.getInstance ().keyBinds ["Ship_fire"]))
				nbBulletThrown = 0;
			GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [nb_sprite * nb_bullet / max_bullet];
			if (hasBuff) {
				remainingBuffTime -= Time.fixedDeltaTime;
				if (remainingBuffTime <= 0)
					EndBonusBourrin ();
			}
		} else {
			if (remainingLoadTime > 0) {
				remainingLoadTime -= Time.fixedDeltaTime;
			} else if (remainingLoadTime <= 0 && cannonRemainingBullets == 0) {
				cannonRemainingBullets = cannonNbBullets;
			} else {
				GameObject bullets = (GameObject)GameObject.Instantiate (Resources.Load ("CannonBullets"));
				bullets.transform.position = transform.position + Vector3.up;
				if(nb_bullet>0)
					nb_bullet--;
				cannonRemainingBullets--;
				if (cannonRemainingBullets <= 0) {
					isShootingCannon = false;
					last_load = Time.fixedTime;
				}
			}
		}
		if (Input.GetKeyDown (InputManager.getInstance ().keyBinds ["Ship_power"]) && hasCannon) {
			isShootingCannon = true;
			remainingLoadTime = loadTime;
			hasCannon = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Block")) {
			Camera.main.gameObject.GetComponent<GameLogicManager> ().EndGame (false);
		}
		if (collider.CompareTag ("Bonus")) {
			if(collider.name=="BonusBourrin(Clone)")
				StartBonusBourrin();
			if(collider.name=="BonusCannon(Clone)")
				StartBonusCannon();
			Destroy (collider.gameObject);
		}
	}

	//Params bonus bourrin

	bool hasBuff=false;
	float buffTime=3;
	float remainingBuffTime=0f;
	float load_rate_mult=1f;
	int min_bullet_add=0;

	void StartBonusBourrin(){
		hasBuff = true;
		remainingBuffTime = buffTime;
		load_rate_mult = 3f;
		min_bullet_add = 2;
	}

	void EndBonusBourrin(){
		hasBuff = false;
		load_rate_mult = 1f;
		min_bullet_add = 0;
	}

	//Params bonus cannon
	bool hasCannon=false;
	bool isShootingCannon=false;
	float loadTime=1f;
	float remainingLoadTime;
	int cannonNbBullets=20;
	int cannonRemainingBullets;

	void StartBonusCannon(){
		hasCannon = true;
	}

}

