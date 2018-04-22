using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour {

	void FixedUpdate () {
		if (transform.position.y > AreaLimits.UpLimit() + 1F || transform.position.y < AreaLimits.BottomLimit() - 1F)
			Destroy (gameObject);
	}
}
