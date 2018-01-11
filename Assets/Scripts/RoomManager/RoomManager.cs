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
	public float riseSpeed	=		0.03f;
	public List<GameObject> potentialRooms;
	public GameObject startRoom;
	public GameObject player;
	public int maxRoomBuffer =	5;
	//public static RoomManager Instance { get; set; }
	
	private GameObject selectedRoom;
	private int rng;
	private Queue<GameObject> loadedRooms;
	
    void Start() {
		Random.InitState(System.Environment.TickCount);
		loadedRooms = new Queue<GameObject>();
        loadedRooms.Enqueue(startRoom);
	}
	
	void Update() {
		//Flood increase rate 0 to 100

		if(player.transform.position.y >= -2f) {
			player.transform.position =
				new Vector2(player.transform.position.x,
							player.transform.position.y - roomSize);
			
			foreach(GameObject room in loadedRooms) {
				room.transform.position =
					new Vector2(0, room.transform.position.y - roomSize);
			}
			
			rng = Random.Range(8, potentialRooms.Count);
			//Instantiate(prefab, transform);
			selectedRoom = Instantiate<GameObject>(potentialRooms[rng], null);
			selectedRoom.transform.position = new Vector2(0, 0);
			loadedRooms.Enqueue(selectedRoom);

			if(loadedRooms.Count == (maxRoomBuffer + 3)) {
				GameObject room = loadedRooms.Dequeue();
				Destroy(room.gameObject);
				//floodPercentage = 0f;
				//floodPosition = 0 (bottom of queue);
			}
		}
		
		//This conditional is where the flood has engulfed a floor.
		else if(true == false) {
			//loadedRooms.Peek().destroy(self);
			loadedRooms.Dequeue();
			//floodPercentage = 0f;
		}
	}
}