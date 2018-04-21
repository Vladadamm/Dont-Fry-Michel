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
			int x=0;
			spawnTimer += spawnInterval / downSpeed;
			Vector3 pos = new Vector3(0, AreaLimits.UpLimit()+1+spawnTimer*downSpeed, 0);
			float randBlock = Random.value;
			string chosenBlock;
			if (randBlock < BlockWeightShort) {
				x=Random.Range(0,AreaLimits.BlockColumnsCount());
				chosenBlock = "BlockShort";
				lastSizeSpawned = 1;
			} else if (randBlock < BlockWeightShort + BlockWeightMedium) {
				x=Random.Range(0,AreaLimits.BlockColumnsCount()-1);
				pos.x += 0.5f;
				chosenBlock = "BlockMedium";
				lastSizeSpawned = 2;
			} else {
				x=Random.Range(1,AreaLimits.BlockColumnsCount()-1);
				chosenBlock = "BlockLong";
				lastSizeSpawned = 3;
			}
			lastColumnSpawned = x;
			GameObject g;
			pos.x += ConvertColumnToPos (x);
			g=(GameObject)GameObject.Instantiate (Resources.Load (chosenBlock));
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

	float ConvertColumnToPos(int x){
		return AreaLimits.CenterX() - AreaLimits.BlockColumnsCount () / 2f + x;
	}

	public float spawnInterval=1;
	public float spawnTimer;
	public float downSpeed=0.2f;


	public int lastSizeSpawned=0;
	public int lastColumnSpawned=0;

	//Block Weights
	public float BlockWeightShort = 0.3f;
	public float BlockWeightMedium = 0.4f;
	public float BlockWeightLong = 0.3f;
}
