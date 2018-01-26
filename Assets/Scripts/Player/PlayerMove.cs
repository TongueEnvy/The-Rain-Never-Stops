using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerMove: MonoBehaviour {
	public AudioSource jump;
	public AudioSource land;
	public GameObject roomManager;
	public GameObject slimeGlob;
	public int globNumber			=	3;
	public float globSpeed			=	4f;
    public float moveSpeed			=	8f;
	public float jumpForce			=	10f;
	public float jumpAcceleration	=	1f;
	public int jumpTimer			=	15;
    public int flungTimer			=	15;
	public GameObject menuManager;
	public int gameOverMenuIndex;
	
	private bool grounded		=	false;	//Player starts midair.
	private bool isJumping		=	false;	//Player isn't jumping at the start.
	private bool hasBeenFlung	=	false;	//Player hasn't been hit yet.
	private bool jumpPressed	=	false;
	private bool jumpReleased	=	false;
	private float horzInput		=	0f;
	private int jumpCounter;
	private int flungCounter;
	private Rigidbody2D body;
	
	//Function is called by spiritingRune
	public void Fling(Vector2 launchVector) {
		isJumping		=	false;
		jumpCounter 	=	0;
        hasBeenFlung	=	true;
        flungCounter	=	flungTimer;
        body.velocity	=	launchVector;
	}
	
	// Use this for initialization
    private void Start() {
        body = gameObject.GetComponent<Rigidbody2D>();
	}
	
	//Update is called onced per frame. Critical for input!
	private void Update() {
        if ((grounded == true) && (Input.GetButtonDown("Jump")) && hasBeenFlung == false)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            isJumping = true;
            jump.Play();
            jumpCounter = jumpTimer;

            for (int i = 0; i < globNumber; i++)
            {
                GameObject newGlob = Instantiate<GameObject>(
                    slimeGlob, gameObject.transform
                );
                newGlob.transform.parent = transform;
                newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(
                    Random.Range(-globSpeed, globSpeed),
                    Random.Range(0, globSpeed)
                );
            }
        }

        jumpReleased	=	Input.GetButtonUp("Jump");
		horzInput		=	Input.GetAxisRaw("Horizontal");
    }
	
	//FixedUpdate is called 50 times per second. Critical for physics!
    private void FixedUpdate() {
        if(hasBeenFlung == true) {
			if(flungCounter > 0) {
				flungCounter--;
			
				if(flungCounter == 0) {
					hasBeenFlung = false;
				}
			}
		}

        else if(hasBeenFlung == false) {
            body.velocity = new Vector2(
                horzInput * moveSpeed,
                body.velocity.y
            );

			if(jumpCounter > 0) {
				jumpCounter--;

				if((jumpCounter == 0) || (jumpReleased == true)) {
					isJumping = false;
					jumpCounter = 0;
				}
				
				else if(isJumping == true) {
					body.velocity = new Vector2(
						body.velocity.x,
						body.velocity.y + jumpAcceleration
					);
				}
			}
        }
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
        
		if(collision.gameObject.tag == "Hazard") {
			Time.timeScale = 0f;
			menuManager.GetComponentInParent<LiteMenuManager>().OpenMenu(
				gameOverMenuIndex,
				true
			);
        }
		
		else if(land.isPlaying == false) {
            land.Play();
        }

        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {

            if (grounded == false)
            {
                for (int i = 0; i < globNumber; i++)
                {
                    var newGlob = Instantiate<GameObject>(slimeGlob, gameObject.transform);
                    newGlob.transform.parent = transform;
                    newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        Random.Range(-globSpeed, globSpeed),
                        Random.Range(0, globSpeed)
                    );
                }
            }

            grounded = true;
            isJumping = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {
            grounded = true;
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Hazard") {
			Time.timeScale = 0f;
			menuManager.GetComponentInParent<LiteMenuManager>().OpenMenu(
				gameOverMenuIndex,
				true
			);
        }

        flungCounter = 0;
        hasBeenFlung = false;
    }
}
