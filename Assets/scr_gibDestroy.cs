using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_gibDestroy : MonoBehaviour {

    float timer = 120;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer -= 1;

        if (timer <= 0)
        {

            Destroy(gameObject);

        }

	}
}
