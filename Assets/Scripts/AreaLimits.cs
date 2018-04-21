using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLimits {

	public static float UpLimit(){
		return Camera.main.ScreenToWorldPoint(new Vector3(0,Camera.main.pixelHeight,0)).y;
	}

	public static float BottomLimit(){
		return Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y;
	}

	public static float LeftLimit(){
		return Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth/4,0,0)).x;
	}

	public static float RightLimit(){
		return Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth*3/4,0,0)).x;
	}

	public static float CenterX(){
		return (LeftLimit () + RightLimit ()) / 2;
	}

	public static float CenterY(){
		return (UpLimit () + BottomLimit ()) / 2;
	}

	public static float SizeX(){
		return (RightLimit () - LeftLimit ());
	}

	public static float SizeY(){
		return (UpLimit () - BottomLimit ());
	}

	public static int BlockColumnsCount(){
		return (int)Mathf.Floor (SizeX ());
	}

	//public const float areaWidth=0.5f;
}
