using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_move : MonoBehaviour {

    Rigidbody2D body;

    [SerializeField] Camera cam;

    [SerializeField] GameObject slimeGlob;
    [SerializeField] int globNumber;
    [SerializeField] float globSpeed;

    bool grounded;
    bool isJumping;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpTimer;
    float jumpCounter;
    [SerializeField] float jumpAcceleration;
    [SerializeField] float moveSpeed;

    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource land;

    // Use this for initialization
    void Start () {

        body = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;

	}

    void OnTriggerEnter2D(Collider2D collision)
    {

        grounded = true;
        isJumping = false;
        for(var i = 0; i < globNumber; i += 1)
        {

            var newGlob = Instantiate<GameObject>(slimeGlob, gameObject.transform);
            newGlob.transform.parent = null;
            newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-globSpeed, globSpeed), Random.Range(0, globSpeed));

        }
        if (land.isPlaying == false){

            land.Play();

        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {

        grounded = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        grounded = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Hazard")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

    // Update is called once per frame
    void Update () {

        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, body.velocity.y);

        if (grounded == true && Input.GetButtonDown("Jump"))
        {

            body.velocity = new Vector2(body.velocity.x, jumpForce);
            isJumping = true;
            jump.Play();
            jumpCounter = jumpTimer;
            for (var i = 0; i < globNumber; i += 1)
            {

                var newGlob = Instantiate<GameObject>(slimeGlob, gameObject.transform);
                newGlob.transform.parent = null;
                newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-globSpeed, globSpeed), Random.Range(0, globSpeed));

            }

        }

        jumpCounter -= 1;

        if (jumpCounter < 0)
        {

            jumpCounter = 0;

        }

        if(jumpCounter == 0 || Input.GetButtonUp("Jump"))
        {

            isJumping = false;

        }

        if (isJumping == true)
        {

            body.velocity = new Vector2(body.velocity.x, body.velocity.y + jumpAcceleration);

        }

        cam.transform.position = new Vector3 (0, transform.position.y, -10);

    }
}
