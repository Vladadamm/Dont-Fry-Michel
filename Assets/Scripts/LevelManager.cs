using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		spawnTimer = spawnInterval / downSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 0; i < transform.childCount; ++i) {
			Transform t = transform.GetChild (i);
			if (Camera.main.WorldToScreenPoint (t.position + Vector3.down).y < 0)
				t.GetComponent<Block> ().Damage (1);
			t.position += Vector3.down*downSpeed * Time.fixedDeltaTime;
		}
		if (spawnTimer < 0) {
			int x=Random.Range(0,Camera.main.pixelWidth);
			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(x, Camera.main.pixelHeight, 10)) + Vector3.up * spawnTimer/downSpeed;
			spawnTimer += spawnInterval / downSpeed;
			GameObject g = (GameObject)GameObject.Instantiate (Resources.Load ("Block"));
			g.transform.position = pos;
			g.transform.SetParent (transform);
		}
		spawnTimer -= Time.fixedDeltaTime;
	}

	public float spawnInterval=1;
	public float spawnTimer;
	public float downSpeed=0.2f;
}
