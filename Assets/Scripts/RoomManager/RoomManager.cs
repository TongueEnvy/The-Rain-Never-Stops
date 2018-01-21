/**************************
** RoomManager.cs
** Chase McWhirt
** January 11th, 2018
***************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager: MonoBehaviour {
	public const float roomSize			=	12f;	//Every room size.
	public List<GameObject> potentialRooms;			//List of available rooms.
	public GameObject startRoom;					//Start room in scene.
	public GameObject freshStartRoom;				//Start room to instantiate.
	public GameObject spawn;						//Player Spawn.
	public GameObject player;						//Player Head.
	public GameObject flood;						//The flood.
	public int maxRoomBuffer			=	5;		//+3 is max rooms between
													//flood and the player.
	public float drownedRoomTime		=	10f;	//Default speed of flood.
	
	private GameObject selectedRoom;				//Room to load in queue.
	private Queue<GameObject> loadedRooms;			//Object of type queue.
	private int rng						=	0;		//An int for RNGesus.
	private float roomDrownedPercent	=	0f;		//Percentage of room sunk.
	private float floodY				=	0f;		//Flood's Y position.
	private Vector3 floodReset			=	new Vector3(0f, (0f - roomSize), 0f);
	private Vector3 floodPosition		=	new Vector3(0f, -15f, 0f);
	private float metersTravelled		=	0f;		//"Score" in meters.
	private float previousYHeight		=	0f;		//Player Y in prev. frame.
	
	//Controls the game restarting instead of reloading the scene.
	public void Restart() {
		//Destroy every room in queue.
		foreach(GameObject deadRoom in loadedRooms) {
			Destroy(deadRoom.gameObject);
			//loadedRooms.Dequeue();
		}
		Debug.Log("Rooms deleted.");
		
		//Pointer set to null. Anything not cleaned up will autoclean now.
		loadedRooms = null;
		
		//Recreate the queue.
		loadedRooms = new Queue<GameObject>();
		
		//Instantiate a fresh start room.
		selectedRoom = Instantiate<GameObject>(freshStartRoom, null);
		
		//Should pretty much be (0f, 0f).
		selectedRoom.transform.position =
			new Vector2(0f, (loadedRooms.Count * 12f));
			
		//Add the fresh start room to queue.
		loadedRooms.Enqueue(selectedRoom);
		Debug.Log("Stage reset.");
		
		//Refresh seed.
		Random.InitState(System.Environment.TickCount);
		
		//Reset percentage to -25%.
		roomDrownedPercent = -25f;
		
		//Reset score reference position.
		previousYHeight = player.transform.position.y;

		//Reset flood's position.
		flood.transform.position = new Vector3(0f, -15f, 0f);
		
		//Change spawn to inside the tower... (Meaningless).
		spawn.transform.position = new Vector3(0f, -3f, 0f);
		
		//Reset player's position.
		player.transform.position = new Vector3(0f, 0f, 0f);
		
		//Ensure this bool is set to true because they're inside the tower.
		player.GetComponent<PlayerCameraControl>().SetPlayerClimbing(true);
		Debug.Log("Game fully reset!");
	}
	
	//This function runs when RoomManager becomes active.
    public void Start() {
		//Sets the seed to something time related, e.g. different every run.
		Random.InitState(System.Environment.TickCount);
		
		//loadedRooms is given a new Queue(data structure) of GameObjects.
		loadedRooms = new Queue<GameObject>();
		
		//startRoom is loaded into the queue.
		loadedRooms.Enqueue(startRoom);
		
		//This is in regards to the percentage of flood from -12 to 0.
		//	Since flood starts at y = -15, it starts at -25%.
		roomDrownedPercent = -25f;
		
		//This is used to track score. Player's y in the previous frame is
		//	set here.
		previousYHeight = player.transform.position.y;
	}
	
	//To sync with the camera, RoomManager updates game mechanics 50
	//	times per second.
	private void Update() {
		//The percentage of flood's progress increases every frame.
        roomDrownedPercent += (Time.deltaTime * 100f / drownedRoomTime);
		
		//Flood's Y changes to match this percentage.
        floodY = ((roomSize * roomDrownedPercent / 100f) - roomSize);
		
		//Using a stand in Vector to store flood's position.
        floodPosition.Set(0f, floodY, 0f);
		
		//Flood's actual position changes.
        flood.transform.position = floodPosition;
		
		//The player's "score" is tracked here as meters ascended.
		metersTravelled += player.transform.position.y - previousYHeight;
		
		//Previous Y height is set to this frame's y for the next frame's calcs.
		previousYHeight = player.transform.position.y;
		//Debug.Log("Meters Travelled: ");
		//Debug.Log(metersTravelled);

		//(These conditionals do queue/game management.)
		//If the player has reached the top of the current room:
		if(player.transform.position.y >=
			(((loadedRooms.Count - 1) * roomSize) - 2f))
		{
			//Generate a random number from 0(inclusive) to the number of rooms
			//	in our list (exclusive).
			rng = Random.Range(0, potentialRooms.Count);
			
			//Instantiate(prefab, transform); <- Slightly better structure <-
			
			//Instantiate our new room.
			selectedRoom = Instantiate<GameObject>(potentialRooms[rng], null);
			
			//Set the new room in the right position.
			selectedRoom.transform.position =
				new Vector2(0f, (loadedRooms.Count * 12f));
			
			//Added the room to our queue for tracking.
			loadedRooms.Enqueue(selectedRoom);
			
			//Hard flood catch up mechanic. Flood is always
			//	(maxRoomBuffer + 3) rooms away.
			if(loadedRooms.Count == (maxRoomBuffer + 3)) {
				//Check level shift for more info.
				LevelShift();
			}
		}
		
		//This conditional is where the flood has engulfed a floor.
		else if(roomDrownedPercent >= 100f) {
			//Check level shift for more info.
			LevelShift();
		}
	}
	
	//Function only called by our game.
	private void LevelShift() {
		//Flood's progress percentage is reset.
		roomDrownedPercent = 0f;
		
		//Flood's actual position is reset.
		flood.transform.position = floodReset;
	
		//DrownedRoom is completely under Flood. Dequeue pops it off the queue.
		//However, popping it off the queue doesn't delete it. We'll set it to
		//	drownedRoom for reference.
		GameObject drownedRoom = loadedRooms.Dequeue();
		
		//Drowned room is destroyed.
		Destroy(drownedRoom.gameObject);

		//Player's position is moved down one floor. (y -= 12)
		player.transform.position =
			new Vector2(
				player.transform.position.x,
				player.transform.position.y - roomSize
			);
		
		//The score tracker is also shifted down a floor to avoid errors.
		previousYHeight -= roomSize;

		//Every room in our queue is moved down a floor. (y -= 12)
		foreach(GameObject room in loadedRooms) {
			room.transform.position =
				new Vector2(0f, room.transform.position.y - roomSize);
		}
	}
}