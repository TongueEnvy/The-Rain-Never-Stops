using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_startClimbing : MonoBehaviour {

    [HideInInspector] public Animator anim;
    public AnimationClip close;
    public GameObject player;

	// Use this for initialization
	void Start () {

        anim = gameObject.GetComponent<Animator>();

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject == player)
        {

            anim.enabled = true;
            anim.Play(close.name);
            player.GetComponent<player_move>().playerIsClimbing = true;

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
