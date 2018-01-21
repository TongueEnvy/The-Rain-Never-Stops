using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_startClimbing: MonoBehaviour {
    public AnimationClip close;
    public GameObject player;
	public GameObject roomManager;
	
	private Animator anim;
	
	// Use this for initialization
	private void Start() {
        anim = gameObject.GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject == player) {
            anim.enabled = true;
            anim.Play(close.name);
            player.GetComponent<PlayerCameraControl>().SetPlayerClimbing(true);
			roomManager.SetActive(true);
        }
    }
}
