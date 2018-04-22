using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {


	Object [] sprites;


	void Start(){
		sprites = Resources.LoadAll ("pipette-sprites-02");
	}

	public void MakeFireSound()
	{
		int a;

		if (a
		MakeSound(explosionSound);
	}

	public void MakeDeathCuistotSound()
	{
		MakeSound(playerShotSound);
	}

	public void MakeHitBlockSound()
	{
		MakeSound(enemyShotSound);
	}
	// Update is called once per frame
	private void MakeSound(AudioSource son)
	{
		son.Play();
	}
}
