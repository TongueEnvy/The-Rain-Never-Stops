using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour {
	public int hudMenuIndex;
	
	public void Update() {
		if(Input.GetButtonDown("Pause")) {
			Time.timeScale = 1f;
			gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
				hudMenuIndex,
				true
			);
        }

        if (Input.GetButton("Submit"))
        {

            OnResumePressed();

        }

        if (Input.GetButton("Cancel"))
        {

            OnQuitPressed();

        }
    }
	
	public void OnResumePressed() {
		Time.timeScale = 1f;
		gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
			hudMenuIndex,
			true
		);
	}

	public void OnQuitPressed()	{
		Debug.Log("Quit button pressed!! Load scene script required!");
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
