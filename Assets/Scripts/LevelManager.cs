using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		spawnTimer = spawnInterval / downSpeed;
		InitSpawn ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateGen (Time.fixedDeltaTime);
	}

	void UpdateGen(float deltaTime){
		for (int i = 0; i < transform.childCount; ++i) {
			Transform t = transform.GetChild (i);
			if (t.position.y - 1 < AreaLimits.BottomLimit())
				t.GetComponent<Block> ().Damage (1);
			t.position += Vector3.down*downSpeed * deltaTime;
		}
		if (spawnTimer < 0) {
			float x=Random.Range(AreaLimits.LeftLimit()+0.5f,AreaLimits.RightLimit()-0.5f);
			Vector3 pos = new Vector3(x, AreaLimits.UpLimit()+1+spawnTimer*downSpeed, 10);
			spawnTimer += spawnInterval / downSpeed;
			GameObject g = (GameObject)GameObject.Instantiate (Resources.Load ("Block"));
			g.transform.position = pos;
			g.transform.SetParent (transform);
		}
		spawnTimer -= deltaTime;
	}

	void InitSpawn(){
		for(int i=0;i<(AreaLimits.UpLimit()-AreaLimits.BottomLimit())/(spawnInterval/downSpeed);++i){
			UpdateGen(spawnInterval/downSpeed);
		}
	}

	public float spawnInterval=1;
	public float spawnTimer;
	public float downSpeed=0.2f;

	//Block Weights
	public float BlockWeightShort = 0.3;
	public float BlockWeightMedium = 0.4;
	public float BlockWeightLong = 0.3;
}
