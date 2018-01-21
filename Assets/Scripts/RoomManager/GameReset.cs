using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Why doesn't this fucking thing work???
public class GameReset: MonoBehaviour {
	public GameObject spawn;
	public GameObject player;
	//public GameObject freshStartRoom;
	//public GameObject door;
	public GameObject flood;
	public GameObject roomManager;
	
	public void SoftReset() {
		//flood.transform.position = new Vector3(-22f, -15f, 0f);
		roomManager.GetComponent<RoomManager>().Restart();
		spawn.transform.position = new Vector3(0f, -3f, 0f);
		//player.transform.position = new Vector3(0f, 0f, 0f);
		//player.GetComponent<PlayerCameraControl>().SetPlayerClimbing(true);

	}
}
