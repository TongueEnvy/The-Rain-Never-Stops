/**************************
** RoomManager.cs
** Chase McWhirt
** January 11th, 2018
***************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerBackup: MonoBehaviour {
	public const float roomSize	=	12f;
	public List<GameObject> potentialRooms;
	public GameObject startRoom;
	public GameObject player;
	public GameObject flood;
	public int maxRoomBuffer	=	5;
	public const float drownedRoomTime	=	5f;
	
	private GameObject selectedRoom;
	private Queue<GameObject> loadedRooms;
	private int rng						=	0;
	private float roomDrownedPercent	=	0f;
	private float floodY				=	0f;
	private Vector3 floodReset			=	new Vector3(0f, (0f - roomSize), 0f);
	private Vector3 floodPosition		=	new Vector3(0f, -15f, 0f);
	
    void Start() {
		Random.InitState(System.Environment.TickCount);
		loadedRooms = new Queue<GameObject>();
        loadedRooms.Enqueue(startRoom);
		roomDrownedPercent = -25f;
	}
	
	void FixedUpdate() {
		//Flood's original speed was 400 frames.
		//At 50FPS (which it's now at) flood's time was 8 seconds.
		//Floods time is now 5 seconds.
		roomDrownedPercent += (Time.deltaTime * 100f / drownedRoomTime);
		floodY = ((roomSize * roomDrownedPercent / 100f) - roomSize);
		floodPosition.Set(0f, floodY, 0f);
		flood.transform.position = floodPosition;

		//If the player has reached the top of the current room:
		if(player.transform.position.y >=
			(((loadedRooms.Count - 1) * roomSize) - 2f))
		{
			rng = Random.Range(0, potentialRooms.Count);
			//Instantiate(prefab, transform); <- Slightly better structure <-
			selectedRoom = Instantiate<GameObject>(potentialRooms[rng], null);
			selectedRoom.transform.position =
				new Vector2(0f, (loadedRooms.Count * 12f));
			loadedRooms.Enqueue(selectedRoom);
			
			//Hard flood catch up mechanic. Flood is always
			//	maxRoomBuffer away.
			if(loadedRooms.Count == (maxRoomBuffer + 3)) {
				LevelShift();
			}
		}
		
		//This conditional is where the flood has engulfed a floor.
		else if(roomDrownedPercent >= 100f) {
			LevelShift();
		}
	}
	
	public void LevelShift() {
		GameObject drownedRoom = loadedRooms.Dequeue();
		Destroy(drownedRoom.gameObject);

		player.transform.position =
			new Vector2(player.transform.position.x,
			player.transform.position.y - roomSize);

		foreach(GameObject room in loadedRooms) {
			room.transform.position =
				new Vector2(0, room.transform.position.y - roomSize);
		}
				
		roomDrownedPercent = 0f;
		flood.transform.position = floodReset;
	}
}