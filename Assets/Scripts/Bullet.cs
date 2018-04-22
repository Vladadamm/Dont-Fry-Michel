using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int vitesse = 300;
	public	int life = 1;
	public int dmg=10;
	Rigidbody2D	rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = new Vector2 (0, vitesse * Time.fixedDeltaTime);
		if (transform.position.y > AreaLimits.UpLimit())
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Block")) {
			collider.transform.GetComponent<Block> ().Damage (dmg);
			life -= 1;
			if (life <= 0)
				Destroy (gameObject);
		}
		if (collider.name == "Fish") {
			collider.transform.GetComponent<Fish> ().SetBoost ();
			life -= 1;
			if (life <= 0)
				Destroy (gameObject);
		}
	}
}
