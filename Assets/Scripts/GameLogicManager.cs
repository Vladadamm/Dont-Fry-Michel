using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		timer = 4f;
		state = 0;
		fish.GetComponent<Rigidbody2D> ().gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0) {
			if (timer > 1)
				startPanel.GetComponentInChildren<Text> ().text = ((int)timer).ToString ();
			else if (timer <= 1 && timer > 0) 
				startPanel.transform.GetChild (1).gameObject.SetActive(true);
			else {
				startPanel.transform.GetChild (1).gameObject.SetActive(false);
				BeginGame ();
			}
			timer -= Time.fixedDeltaTime;
		}
		if (state == 2) {
			if (Input.GetKeyDown (InputManager.getInstance ().keyBinds ["Escape"]) || Input.GetKeyDown(InputManager.getInstance().keyBinds["Enter"]))
				SceneManager.LoadScene (0);
		}
	}

	void BeginGame(){
		startPanel.SetActive (false);
		fish.GetComponent<Rigidbody2D> ().gravityScale = 1;
		fish.GetComponent<Fish> ().enabled = true;
		pipette.GetComponent<Ship> ().enabled = true;
		levelManager.GetComponent<LevelManager> ().enabled = true;
		state = 1;
	}

	public void EndGame(bool p){
		fish.GetComponent<Rigidbody2D> ().gravityScale = 0;
		fish.GetComponent<Fish> ().enabled = false;
		pipette.GetComponent<Ship> ().enabled = false;
		levelManager.GetComponent<LevelManager> ().enabled = false;
		endPanel.SetActive (true);
		if (p) {
			endPanel.transform.GetChild (0).gameObject.SetActive(true);
		} else {
			endPanel.transform.GetChild (1).gameObject.SetActive(true);
		}
		state = 2;
	}

	public int state;

	private float timer;

	public GameObject fish;
	public GameObject pipette;
	public GameObject startPanel;
	public GameObject endPanel;
	public GameObject levelManager;
}
