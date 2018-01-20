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
	
	private bool grounded		=	false;	//Player starts midair.
	private bool isJumping		=	false;	//Player isn't jumping at the start.
	private bool hasBeenFlung	=	false;	//Player hasn't been hit yet.
	private int jumpCounter;
	private int flungCounter;
	private Rigidbody2D body;
	
	//Function is called by spiritingRune
	public void Fling(Vector2 launchVector) {
		isJumping = false;
		jumpCounter = 0;
        hasBeenFlung = true;
        flungCounter = flungTimer;
        body.velocity = launchVector;
	}
	
	// Use this for initialization
    private void Start() {
        body = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
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
                (Input.GetAxisRaw("Horizontal") * moveSpeed),
                body.velocity.y
            );

			if(jumpCounter > 0) {
				jumpCounter--;

				if(jumpCounter == 0 || Input.GetButtonUp("Jump")) {
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
			
            else if((grounded == true) && (Input.GetButtonDown("Jump"))) {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                isJumping = true;
				grounded = false;
                jump.Play();
                jumpCounter = jumpTimer;

                for(int i = 0; i < globNumber; i++) {
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
        }
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
        grounded = true;
        isJumping = false;
        
		if(collision.gameObject.tag == "Hazard") {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			roomManager.GetComponent<RoomManager>().Restart();
			//transform.position = new Vector3(0f, 0f, 0f);
        }
		
		else if(land.isPlaying == false) {
            land.Play();
        }
		
		for(int i = 0; i < globNumber; i++) {
            var newGlob = Instantiate<GameObject>(slimeGlob, gameObject.transform);
			newGlob.transform.parent = transform;
            newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(
				Random.Range(-globSpeed, globSpeed),
				Random.Range(0, globSpeed)
			);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Hazard") {
			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			roomManager.GetComponent<RoomManager>().Restart();
			//transform.position = new Vector3(0f, 0f, 0f);

        }

        flungCounter = 0;
        hasBeenFlung = false;
    }
}
