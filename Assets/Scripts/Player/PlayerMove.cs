using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour {
	public AudioSource jump;
	//public AudioSource land;
	public GameObject slimeGlob;
	public int globNumber;
	public float globSpeed;
    public float moveSpeed;
	public float jumpForce;
	public float jumpAcceleration;
	public int jumpTimer;
    public int flungTimer;
	
	private bool grounded		=	false;	//Player starts midair.
	private bool isJumping		=	false;	//Player isn't jumping at the start.
	private bool hasBeenFlung	=	false;	//Player hasn't been hit yet.
	private int jumpCounter;
	private int flungCounter;
	private Rigidbody2D body;
	
	// Use this for initialization
    private void Start() {
        body = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    private void FixedUpdate() {
        if(jumpCounter > 0) {
            jumpCounter--;
			
			if(jumpCounter == 0 || Input.GetButtonUp("Jump")) {
				isJumping = false;
			}
        }
		
		else if(flungCounter > 0) {
			flungCounter--;
			
			if(flungCounter == 0) {
				hasBeenFlung = false;
			}
		}

        else if(hasBeenFlung == false) {
            /*body.velocity = new Vector2(
                (Input.GetAxisRaw("Horizontal") * moveSpeed),
                body.velocity.y
            );*/
			body.velocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

            if((grounded == true) && (Input.GetButtonDown("Jump"))) {
                //body.velocity = new Vector2(body.velocity.x, jumpForce);
                body.velocity.y = jumpForce;
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
			
			else if(isJumping == true) {
				body.velocity = new Vector2(
					body.velocity.x,
					body.velocity.y + jumpAcceleration
				);
			}
        }
	}
}
