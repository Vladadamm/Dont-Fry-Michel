using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour {

	// Use this for initialization
	void OnEnable(){
		GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ().enabled = false;
		//foreach(GameObject.FindGameObjectsWithTag("Player"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
