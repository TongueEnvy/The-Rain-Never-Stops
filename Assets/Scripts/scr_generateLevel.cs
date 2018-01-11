using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_generateLevel: MonoBehaviour {
    public float roomSize;
    public int roomNumber;
    public List<GameObject> potentialRooms;
    public GameObject exit;

	void Start () {
        Random.InitState(System.Environment.TickCount);
        var newRoomPoz = roomSize;
        for(var i = 1; i < roomNumber; i += 1) {
            var newRoom = Instantiate<GameObject>(potentialRooms[Random.Range(0, potentialRooms.Count)], null);
            newRoom.transform.position = new Vector2(0, newRoomPoz);
            newRoomPoz += roomSize;
        }

        var finalRoom = Instantiate<GameObject>(exit, null);
        finalRoom.transform.position = new Vector2(0, newRoomPoz);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
