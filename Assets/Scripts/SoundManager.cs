using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public	AudioClip[] Fire_audio;
	public	AudioClip[] DeathCuisto_audio;
	public	AudioClip[] HitBlock_audio;
	public	AudioClip[] BonusCuisto_audio;
	float	Fire_time;
	float	HitBlock_time;
	float	DeathCuisto_time;
	float	BonusCuisto_time;


	public void MakeFireSound()
	{

		if (Time.fixedTime > Fire_time + 0.1f) {
			MakeSound (Fire_audio [Random.Range (0, 4)]);
			Fire_time = Time.fixedTime;
		}
	}

	public  void MakeDeathCuistotSound()
	{
		if (Time.fixedTime > DeathCuisto_time + 0.1f) {
			MakeSound(DeathCuisto_audio[Random.Range(0, 3)]);
			DeathCuisto_time = Time.fixedTime;
		}
	}

	public void MakeHitBlockSound(){
		if (Time.fixedTime > HitBlock_time + 0.1f) {
			MakeSound(HitBlock_audio [Random.Range (0, 5)]);
			HitBlock_time = Time.fixedTime;
		}
	}

	public void MakeBonusSound(){
		if (Time.fixedTime > BonusCuisto_time + 0.1f) {
			MakeSound(HitBlock_audio[Random.Range(0, 5)]);
			BonusCuisto_time = Time.fixedTime;
		}
	}


	private void MakeSound(AudioClip son)
	{
		AudioSource.PlayClipAtPoint(son, GetComponent<Camera>().transform.position);
		GetComponent<AudioSource>().clip = son;
		GetComponent<AudioSource>().Play();

	}
}
