/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_theFlood: MonoBehaviour {
    public float riseSpeed;
    public GameObject player;
    public float playerPosMod;

    // Update is called once per frame
    void Update() {
        if (player.GetComponent<player_move>().playerIsClimbing == true) {
            transform.position = new Vector2(
				transform.position.x,
                transform.position.y + (
					//Very soft catch up mechanic.
					riseSpeed +
                    (player.transform.position.y * playerPosMod)
                )
            );
        }
    }
}*/
