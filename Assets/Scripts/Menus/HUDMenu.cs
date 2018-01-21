using UnityEngine;

public class HUDMenu: MonoBehaviour {
	public int pauseMenuIndex;

	public void PauseMenuOpen() {
		Time.timeScale = 0f;
		gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
			pauseMenuIndex,
			true
		);
	}
	
	private void Start() {
		Time.timeScale = 1f;
	}
	
	private void Update() {
		if(Input.GetButtonDown("Pause")) {
			Time.timeScale = 0f;
			gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
				pauseMenuIndex,
				true
			);
		}
	}
}
