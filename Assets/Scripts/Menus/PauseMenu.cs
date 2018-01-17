using UnityEngine;

public class PauseMenu: MonoBehaviour {
	//public GameObject liteMenuManager;
	public int hudMenuIndex;
	
	public void OnResumePressed() {
		Time.timeScale = 1f;
		gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
			hudMenuIndex,
			true
		);
		//liteMenuManager.GetComponent<LiteMenuManager>().CloseMenu();
	}

	public void OnQuitPressed()	{
		Debug.Log("Quit button pressed!! Load scene script required!");
		//Hide();
		//Destroy(this.gameObject); // This menu does not automatically destroy itself

		//GameMenu.Hide();
	}
}
