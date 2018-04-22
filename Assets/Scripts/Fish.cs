using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

	public	GameObject	Michel2;
	public	float	size_sprite = 0.2f;
	public Rigidbody2D rb;
	public int	jump = 300;
	public int	move = 100;
	float boost = 1;
	bool is_jump;
	float x;
	float y;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (AreaLimits.CenterX(), AreaLimits.CenterY()+2, 0);
		rb = GetComponent<Rigidbody2D>();
		is_jump = false;
		enabled = false;
		//Michel2.SetActive(false);
	}

	// Update is called once per frame
	void FixedUpdate () {
		InputManage ();
		if (transform.position.x < AreaLimits.LeftLimit () + size_sprite) {
			if (transform.position.x < AreaLimits.LeftLimit () - size_sprite) {
				transform.position = new Vector3 (AreaLimits.RightLimit () - size_sprite, transform.position.y);
				//Michel2.SetActive (false);
			}/* else {
					Michel2.SetActive (true);
					transform.position = new Vector3 (AreaLimits.LeftLimit () + size_sprite, transform.position.y);
				}*/
		} else if (transform.position.x > AreaLimits.RightLimit () - size_sprite){

			if ((transform.position.x > AreaLimits.RightLimit () + size_sprite)) {
				transform.position = new Vector3 (AreaLimits.LeftLimit () + size_sprite, transform.position.y);
				//Michel2.SetActive (false);
				//Michel2.
			} /*else {
					Michel2.SetActive (true);
					Michel2.transform.position = new Vector3 (AreaLimits.LeftLimit () + size_sprite, transform.position.y);
				}*/
		}
		if (transform.position.y < AreaLimits.BottomLimit () - size_sprite) {
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
				x =  boost *  -move * Time.fixedDeltaTime;
			}
				if (Input.GetKey (InputManager.getInstance ().keyBinds ["Fish_jump"]) && is_jump == false) {
				y =   jump * Time.fixedDeltaTime;
				is_jump = true;
			}
			rb.velocity = new Vector2 (x, y);

		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		is_jump = false;
	}

	public void SetBoost()
	{
		boost += 0.1f;
	}

	void OnDisable(){
		rb.velocity = Vector2.zero;
	}
}
