using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spiritingRune: MonoBehaviour {
    public GameObject weilder;
    public float launchForce;
    public bool locked;

    private Vector2 launchDirection;
    private float lockedAngle;

	// Use this for initialization
	void Start() {
        lockedAngle = transform.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update() {
        launchDirection = transform.up;
        launchDirection.Normalize();

        if(locked == true) {
            transform.eulerAngles = new Vector3(0, 0, lockedAngle);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if(collision.gameObject.GetComponent<player_move>() != null) {
                collision.gameObject.GetComponent<PlayerMove>().Fling(
					launchDirection * launchForce
				);
            }
		}
    }
}
