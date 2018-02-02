using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_twitch : MonoBehaviour {

    public GameObject sprite;
    public float minTwitchTimer;
    public float maxTwitchTimer;
    public float maxTwitchOffset;

    float twitchTimer;

	// Use this for initialization
	void Start () {

        twitchTimer = Random.Range(minTwitchTimer, maxTwitchTimer);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        twitchTimer -= Time.deltaTime;

        if (twitchTimer <= 0)
        {

            sprite.transform.position = new Vector2(transform.position.x + Random.Range(-maxTwitchOffset,maxTwitchOffset), transform.position.y + Random.Range(-maxTwitchOffset, maxTwitchOffset));

        }

	}
}
