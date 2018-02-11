using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_projectileMove : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {

        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
