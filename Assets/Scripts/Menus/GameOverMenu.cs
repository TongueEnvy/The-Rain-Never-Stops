using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu: MonoBehaviour {
	public int hudMenuIndex;
	public GameObject roomManager;
    public GameObject player;

    private void Update()
    {

        if (Input.GetButton("Submit"))
        {

            OnRetryPressed();

        }

        if (Input.GetButton("Cancel"))
        {

            OnQuitPressed();

        }

    }

    public void OnRetryPressed() {
        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        player.GetComponent<PlayerMove>().segment0.SetActive(true);
        player.GetComponent<PlayerMove>().segment1.SetActive(true);
        

        Destroy(GameObject.Find("playerSkull(Clone)"));

        roomManager.GetComponent<RoomManager>().Restart();
		gameObject.GetComponentInParent<LiteMenuManager>().OpenMenu(
			hudMenuIndex,
			true
		);
	}

	public void OnQuitPressed()	{
		Debug.Log("Quit button pressed!! Load scene script required!");
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
