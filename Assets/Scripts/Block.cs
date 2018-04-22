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
			float rand=Random.value;
			if (rand < bonusBourrinWeight && transform.position.y - 1 > AreaLimits.BottomLimit()) {
				GameObject g = (GameObject)GameObject.Instantiate (Resources.Load ("BonusBourrin"));
				g.transform.position = transform.position;
			}else if (rand < (bonusBourrinWeight + bonusCannonWeight) && transform.position.y - 1 > AreaLimits.BottomLimit()) {
				GameObject g = (GameObject)GameObject.Instantiate (Resources.Load ("BonusCannon"));
				g.transform.position = transform.position;
			}
			GameObject.Destroy (gameObject);
		} else {
			linear_color ();
			//transform.GetComponent<SpriteRenderer>().color = Color.white * (health / (float)maxHealth);
		}
	}

	void linear_color()
	{
		float b = transform.GetComponent<SpriteRenderer> ().color.b;
		float r = transform.GetComponent<SpriteRenderer> ().color.r;
		float g = transform.GetComponent<SpriteRenderer> ().color.g;
		float mult = health / ((float)maxHealth);
		if (b > 0)
		b *= mult;
		if (r > 0)
			r *= mult;
		if (g > 0)
		g *= mult;
		transform.GetComponent<SpriteRenderer> ().color = new Color(r,g,b);
		
	}

	public int maxHealth;
	public int health;

	public float bonusBourrinWeight = 0.1f;
	public float bonusCannonWeight = 0.05f;
}
