using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
		health = maxHealth;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage(int dmg){
		health -= dmg;
		if (health <= 0) {
			GameObject.Destroy (gameObject);
		} else {
			transform.GetComponent<SpriteRenderer>().color = Color.white * (health / (float)maxHealth);
		}
	}

	public int maxHealth;
	public int health;
}
