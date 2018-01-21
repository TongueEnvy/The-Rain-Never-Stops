using UnityEngine;

public class HUDMenu: MonoBehaviour {
	//public GameObject liteMenuManager;
	public int pauseMenuIndex;

	public void PauseMenuOpen() {
		Debug.Log("Pause button pressed!!");
		Time.timeScale = 0f;
		gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
			pauseMenuIndex,
			true
		);
		//liteMenuManager.GetComponent<LiteMenuManager>().OpenMenu(pauseMenuIndex, true);
		//PauseMenu.Show();
	}
}
