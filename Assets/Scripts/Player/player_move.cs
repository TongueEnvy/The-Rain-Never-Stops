using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class player_move: MonoBehaviour {
	public GameObject slimeGlob;
	public int globNumber;
	public float globSpeed;
    [HideInInspector]public bool isJumping;
    public float jumpForce;
	public float jumpTimer;
	public float jumpAcceleration;
    [HideInInspector]public bool hasBeenFlung;
    public float flungTimer;
    [HideInInspector]public float flungCounter;
    public float moveSpeed;
	public AudioSource jump;
	public AudioSource land;
    public GameObject theFlood;
	public GameObject hudMenu;
	
	private Rigidbody2D body;
	private bool grounded;
	private bool isCameraSet;
	private float jumpCounter;
	
    // Use this for initialization
    
    void Start () {
        hasBeenFlung = false;

        body = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
        playerIsClimbing = false;
	}

    void OnTriggerEnter2D(Collider2D collision) {
        grounded = true;
        isJumping = false;
        
		if(collision.gameObject.tag == "Hazard") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
		
		for(int i = 0; i < globNumber; i++) {
            var newGlob = Instantiate<GameObject>(slimeGlob, gameObject.transform);
			newGlob.transform.parent = transform;
            newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(
				Random.Range(-globSpeed, globSpeed),
				Random.Range(0, globSpeed)
			);
        }
		
        if (land.isPlaying == false) {
            land.Play();
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision) {
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Hazard") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        flungCounter = 0;

        hasBeenFlung = false;
    }
}
