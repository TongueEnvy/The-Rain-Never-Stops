using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_theFlood : MonoBehaviour {

    public float riseSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector2 (transform.position.x, transform.position.y + riseSpeed);
        riseSpeed += .000015f;

	}
}
