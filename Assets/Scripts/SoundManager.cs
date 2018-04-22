using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {

	Object[] audio;


	void Start(){
		audio = Resources.LoadAll("SOUND"); 
	}

	public void MakeFireSound()
	{
		MakeSound((AudioSource) audio[7 + Random.Range (0, 4)]);
	}

	public void MakeDeathCuistotSound()
	{
		MakeSound((AudioSource)audio[1]);
	}

	public void MakeHitBlockSound()
	{
		MakeSound((AudioSource)audio[2]);
	}

	private void MakeSound(AudioSource son)
	{
		son.Play();
	}
}
