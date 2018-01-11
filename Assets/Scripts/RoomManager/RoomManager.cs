/**************************
** RoomManager.cs
** Chase McWhirt
** January 10th, 2018
***************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager: MonoBehaviour {
	public const float roomSize	=	12f;
	//public float riseSpeed	=	0.03f;
	public List<GameObject> potentialRooms;
	public GameObject startRoom;
	public GameObject player;
	public GameObject flood;
	public int maxRoomBuffer	=	5;
	//public static RoomManager Instance { get; set; }
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
	
	void Update() {
		//Flood increase rate 0 to 100
		//At a speed of 0.03 meters per frame,
		//	the flood rises from one floor to the next
		//	in 400 frames. There is an extra 100 frames
		//	at the start. Thus, every frame, it should
		//	cover 0.25% more of the flood.
		Debug.Log(1.0f / Time.deltaTime);
		roomDrownedPercent += (Time.deltaTime * 100f / drownedRoomTime);
		floodY = ((roomSize * roomDrownedPercent / 100f) - roomSize);
		Debug.Log(floodY);
		floodPosition.Set(0f, floodY, 0f);
		Debug.Log(floodPosition.y);
		flood.transform.position = floodPosition;

		if(player.transform.position.y >=
			(((loadedRooms.Count - 1) * roomSize) - 2f))
		{
			rng = Random.Range(0, potentialRooms.Count);
			//Instantiate(prefab, transform); <- Slightly better structure <-
			selectedRoom = Instantiate<GameObject>(potentialRooms[rng], null);
			selectedRoom.transform.position =
				new Vector2(0, (loadedRooms.Count * 12f));
			loadedRooms.Enqueue(selectedRoom);

			if(loadedRooms.Count == (maxRoomBuffer + 3)) {
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
		
		//This conditional is where the flood has engulfed a floor.
		else if(roomDrownedPercent >= 100f) {
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
}