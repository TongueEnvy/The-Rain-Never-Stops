using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spiritingRune : MonoBehaviour {

    public GameObject weilder;
    public float launchForce;

    [HideInInspector] public Vector2 launchDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        launchDirection = transform.position - weilder.transform.position;
        launchDirection.Normalize();

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {

            if(collision.gameObject.GetComponent<player_move>() != null)
            {
                collision.gameObject.GetComponent<player_move>().isJumping = false;
                collision.gameObject.GetComponent<player_move>().hasBeenFlung = true;
                collision.gameObject.GetComponent<player_move>().flungCounter = collision.gameObject.GetComponent<player_move>().flungTimer;
            }

            collision.GetComponent<Rigidbody2D>().velocity = launchDirection * launchForce;
        }

    }
}
