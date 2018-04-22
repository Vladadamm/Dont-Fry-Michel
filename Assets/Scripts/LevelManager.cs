using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		spawnTimer = spawnInterval / downSpeed;
		InitSpawn ();
		enabled = false;
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
		if (spawnTimer <= 0) {
			downSpeed += downSpeedIncrement;
			spawnTimer += spawnInterval / downSpeed;
			float randEmptyLane = Random.value;
			if (randEmptyLane >= emptyLaneWeight || isLastEmpty) {
				isLastEmpty = false;
				int x = 0;
				Vector3 pos = new Vector3 (0, AreaLimits.UpLimit () + 1 + spawnTimer * downSpeed, 0);
				float randBlock = Random.value;
				string chosenBlock;
				if (randBlock < BlockWeightShort) {
					//Bloc court couvrant bloc court : pas possible
					if (lastSizeSpawned == 1) {
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 1);
						if (x >= lastColumnSpawned)
							++x;
					} else {
						x = Random.Range (0, AreaLimits.BlockColumnsCount ());
					}
					chosenBlock = "BlockShort";
					lastSizeSpawned = 1;
				} else if (randBlock < BlockWeightShort + BlockWeightMedium) {
					//Bloc medium couvrant bloc medium : pas possible
					if (lastSizeSpawned == 2) {
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 2);
						if (x >= lastColumnSpawned)
							++x;
					} else if (lastSizeSpawned == 1) {
						//Bloc medium couvrant bloc court : pas possible
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 3);
						if (x >= lastColumnSpawned - 1)
							x += 2;
						;
					} else {
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 1);
					}
					pos.x += 0.5f;
					chosenBlock = "BlockMedium";
					lastSizeSpawned = 2;
				} else {
					//Bloc long couvrant bloc long : pas possible
					if (lastSizeSpawned == 3) {
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 3);
						if (x >= lastColumnSpawned)
							++x;
					} else if (lastSizeSpawned == 2) {
						//Bloc long couvrant bloc medium : pas possible
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 4);
						if (x >= lastColumnSpawned - 1)
							x += 2;
						;
					} else if (lastSizeSpawned == 1) {
						//Bloc long couvrant bloc court : pas possible
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 5);
						if (x >= lastColumnSpawned - 2)
							x += 3;
					} else {
						x = Random.Range (0, AreaLimits.BlockColumnsCount () - 2);
					}
					pos.x += 1f;
					chosenBlock = "BlockLong";
					lastSizeSpawned = 3;
				}
				lastColumnSpawned = x;
				GameObject g;
				pos.x += ConvertColumnToPos (x);
				g = (GameObject)GameObject.Instantiate (Resources.Load (chosenBlock));
				g.transform.position = pos;
				g.transform.SetParent (transform);
			} else {
				isLastEmpty = true;
			}
		}
		spawnTimer -= deltaTime;
	}

	void InitSpawn(){
		Vector3 pos = new Vector3(ConvertColumnToPos(AreaLimits.BlockColumnsCount()/2), AreaLimits.UpLimit()+1+spawnTimer*downSpeed, 0);
		GameObject g;
		g=(GameObject)GameObject.Instantiate (Resources.Load ("BlockLong"));
		g.transform.position = pos;
		g.transform.SetParent (transform);
		lastColumnSpawned = AreaLimits.BlockColumnsCount()/2;
		lastSizeSpawned = 3;
		isLastEmpty = true;
		for(int i=0;i<AreaLimits.SizeY()/2;++i){
			UpdateGen(spawnInterval/downSpeed);
		}
		spawnTimer = spawnInterval / downSpeed;
	}

	float ConvertColumnToPos(int x){
		return AreaLimits.CenterX() - AreaLimits.BlockColumnsCount () / 2f + x;
	}

	public float spawnInterval=1;
	public float spawnTimer;
	public float downSpeed=0.2f;

	public float downSpeedIncrement=0.001f;


	public int lastSizeSpawned=0;
	public int lastColumnSpawned=0;

	public float emptyLaneWeight=0.4f;
	public bool isLastEmpty;

	//Block Weights
	public float BlockWeightShort = 0.2f;
	public float BlockWeightMedium = 0.45f;
	public float BlockWeightLong = 0.35f;
}
