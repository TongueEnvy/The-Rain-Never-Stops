using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewTitleMenu: MonoBehaviour {
    public void OnNewGamePressed() {
		SceneManager.LoadScene(1, LoadSceneMode.Single);
		//Debug.Log("New Game button was pressed.");
		//Scene 1 Load
	}

	public void OnLevelEditorPressed() {
		//Debug.Log("Level Editor button was pressed.");
	}
	
	public void OnSettingsPressed() {
		//SettingsMenu.Show();
		//Debug.Log("Settings button was pressed.");
	}
	
	public void OnQuitPressed() {
		//Debug.Log("Quit button was pressed.");
		Application.Quit();
	}
}
