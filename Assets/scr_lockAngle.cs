using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_lockAngle : MonoBehaviour {

    public float lockAngle;
    public GameObject parentedTo;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {

        transform.parent = null;
        transform.eulerAngles = new Vector3(0, 0, lockAngle);
        transform.parent = parentedTo.transform;

	}
}
