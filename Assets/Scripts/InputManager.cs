using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager {

	private static InputManager instance;

	public static InputManager getInstance()
	{
		if (instance == null) {
			instance = new InputManager();
		}
		return instance;
	}

	public Dictionary<string, KeyCode> keyBinds{get; set;}

	// Use this for initialization
	private InputManager () {
		keyBinds = new Dictionary<string, KeyCode> ();
		keyBinds.Add ("Up", KeyCode.Z);
		keyBinds.Add ("Down", KeyCode.S);
		keyBinds.Add ("Left", KeyCode.Q);
		keyBinds.Add ("Right", KeyCode.D);
		keyBinds.Add ("Space", KeyCode.Space);
		keyBinds.Add ("Esc", KeyCode.Escape);
		keyBinds.Add ("LeftClick", KeyCode.Mouse0);
		keyBinds.Add ("RightClick", KeyCode.Mouse1);
	}
	
	//Redéfinition d'un keyBind 
	public void bindKey(string key, KeyCode keyBind){
		if (!keyBinds.ContainsValue(keyBind)) {
			keyBinds [key] = keyBind;
		}else{
			string tmpK = keyBinds.FirstOrDefault(x => x.Value == keyBind).Key;
			KeyCode tmpKB = keyBinds [key];
			keyBinds [key] = keyBind;
			keyBinds [tmpK] = tmpKB;
		}
	}
}
