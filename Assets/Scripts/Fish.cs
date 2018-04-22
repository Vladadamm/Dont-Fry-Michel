using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

	public	GameObject	Michel2;
	public	float	size_sprite = 0.2f;
	public Rigidbody2D rb;
	public int	jump = 300;
	public int	move = 100;
	float boost = 1f;
	bool is_jump;
	float x;
	float y;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (AreaLimits.CenterX(), AreaLimits.CenterY()+2, 0);

		rb = GetComponent<Rigidbody2D>();
		is_jump = false;
		enabled = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		InputManage ();
		if (transform.position.x < AreaLimits.LeftLimit () + size_sprite) {
			if (transform.position.x < AreaLimits.LeftLimit () - size_sprite) {
				transform.position = new Vector3 (AreaLimits.RightLimit () - size_sprite, transform.position.y);
			}
		} else if (transform.position.x > AreaLimits.RightLimit () - size_sprite){

			if ((transform.position.x > AreaLimits.RightLimit () + size_sprite)) {
				transform.position = new Vector3 (AreaLimits.LeftLimit () + size_sprite, transform.position.y);
			}
		}
		if (transform.position.y < AreaLimits.BottomLimit () - size_sprite) {
			transform.GetComponentInChildren<Animator> ().enabled = false;
			transform.GetComponentInChildren<SpriteRenderer> ().color = new Color(0, 0, 0);
			Camera.main.gameObject.GetComponent<SoundManager> ().MakeDeathFishSound();
			Camera.main.gameObject.GetComponent<GameLogicManager> ().EndGame (true);
		}
	}


	void InputManage(){
		if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_right"]) || Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_left"]) || (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_jump"]) && is_jump == false)) {
			y = rb.velocity.y;
			x = rb.velocity.x;
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_right"])) {
				x = boost *  move * Time.fixedDeltaTime;
			}
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_left"])) {
				x =  boost * -move * Time.fixedDeltaTime;
			}
			if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_jump"]) && is_jump == false) {
				Camera.main.gameObject.GetComponent<SoundManager> ().MakeJumpFishSound();
				y =   jump * Time.fixedDeltaTime;
				is_jump = true;
			}
			rb.velocity = new Vector2 (x, y);

		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (is_jump == true)
			Camera.main.gameObject.GetComponent<SoundManager> ().MakeHitFishSound();
		is_jump = false;
	}

	public void SetBoost()
	{
		boost += 0.01f;
	}

	void OnDisable(){
		rb.velocity = Vector2.zero;
	}
}
