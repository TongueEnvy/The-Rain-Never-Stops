using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class player_move: MonoBehaviour {
    public Camera cam;
    public float camSpeed;
    public bool playerIsClimbing;
	public GameObject slimeGlob;
	public int globNumber;
	public float globSpeed;
	public float jumpForce;
	public float jumpTimer;
	public float jumpAcceleration;
	public float moveSpeed;
	public AudioSource jump;
	public AudioSource land;
    public GameObject theFlood;
	
	private Rigidbody2D body;
	private bool grounded;
	private bool isJumping;
	private float jumpCounter;
	
    // Use this for initialization
    void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
        playerIsClimbing = false;
	}

    void OnTriggerEnter2D(Collider2D collision) {
        grounded = true;
        isJumping = false;
        for(int i = 0; i < globNumber; i++) {
            var newGlob = Instantiate<GameObject>(slimeGlob, gameObject.transform);
            newGlob.transform.parent = null;
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
    }

    // Update is called once per frame
    void Update () {
        body.velocity = new Vector2(
			(Input.GetAxisRaw("Horizontal") * moveSpeed),
			body.velocity.y
		);

        if (grounded == true && Input.GetButtonDown("Jump")) {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            isJumping = true;
            jump.Play();
            jumpCounter = jumpTimer;
            
			for (int i = 0; i < globNumber; i++) {
                var newGlob =
					Instantiate<GameObject>(slimeGlob, gameObject.transform);
                newGlob.transform.parent = null;
                newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(
					Random.Range(-globSpeed, globSpeed),
					Random.Range(0, globSpeed)
				);
            }
        }

        jumpCounter -= 1;

        if(jumpCounter < 0) {
            jumpCounter = 0;
        }

        if(jumpCounter == 0 || Input.GetButtonUp("Jump")) {
            isJumping = false;
        }

        if(isJumping == true) {
            body.velocity = new Vector2(
				body.velocity.x,
				body.velocity.y + jumpAcceleration
			);
        }

        if (playerIsClimbing == false)
        {

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), camSpeed);

        }
        else
        {

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(0, transform.position.y, -10), camSpeed);

        }
        
        theFlood.transform.position = new Vector2(transform.position.x, theFlood.transform.position.y);
    }
}
