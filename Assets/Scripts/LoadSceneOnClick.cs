using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	private const int MENU_ID = 0;
	private const int GAME_ID = 2;
	private const int EDITOR_ID = 1;


	public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

	public void LaunchMenu(){
		SceneManager.LoadScene(MENU_ID);
	}

	public void LaunchGame(){
		SceneManager.LoadScene(GAME_ID);
	}

	public void LaunchEditor(){
		SceneManager.LoadScene(EDITOR_ID);
	}

	public void LaunchEditorNewMap(){
		SaveLoadMap.selectedFile = "temp";
		SceneManager.LoadScene(EDITOR_ID);
	}
}
