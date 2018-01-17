using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_rotate : MonoBehaviour {

    public float rotSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotSpeed);

	}
}
