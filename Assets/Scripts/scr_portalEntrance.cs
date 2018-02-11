using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_portalEntrance : MonoBehaviour {

    public GameObject exit;
    [HideInInspector] public GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("playerHead");

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject == player)
        {

            player.transform.position = exit.transform.position;

        }

    }

}
