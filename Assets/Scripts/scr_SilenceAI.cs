using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_SilenceAI : MonoBehaviour {

    Animator anim;
    bool grounded;

    public AnimationClip walkLeft;
    public AnimationClip walkRight;
    public AnimationClip jump;

    GameObject target;
    public GameObject avatar;
    Rigidbody2D rb;

    public GameObject aggroMeter;
    public List<Sprite> aggroPhases;
    public float walkSpeed;
    public float jumpForce;
    public float jumpTimer;
    public float aggroRange;

    float distanceFromTarget;
    float jumpCounter;

    // Use this for initialization
    void Start()
    {

        anim = avatar.GetComponent<Animator>();
        target = GameObject.Find("playerHead");
        aggroMeter.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpCounter = jumpTimer;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Ground" || collision.tag == "Enemy")
        {

            grounded = true;

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {

            grounded = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        grounded = false;

    }

	// Update is called once per frame
	void FixedUpdate () {

        distanceFromTarget = Vector2.Distance(transform.position, target.transform.position);

        if(distanceFromTarget <= aggroRange)
        {
            jumpCounter -= Time.deltaTime;

            aggroMeter.SetActive(true);

            if (jumpCounter < jumpTimer / 4)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[3];

            }
            else if (jumpCounter < (jumpTimer / 4) * 2)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[2];

            }
            else if (jumpCounter < (jumpTimer / 4) * 3)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[1];

            }
            else if (jumpCounter >= (jumpTimer / 4) * 3)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[0];

            }

            if (target.transform.position.x > transform.position.x)
            {

                if (grounded == true)
                {
                    anim.Play(walkRight.name);
                }

                rb.velocity = new Vector2(walkSpeed, rb.velocity.y);

            }
            else
            {

                if(grounded == true)
                {
                    anim.Play(walkLeft.name);
                }

                rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);

            }

            if (jumpCounter <= 0)
            {

                if (grounded == true)
                {

                    anim.Play(jump.name);
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                }

                jumpCounter = jumpTimer;

            }

        }
        else if (distanceFromTarget > aggroRange)
        {

            aggroMeter.SetActive(false);
            jumpCounter = jumpTimer;

        }

    }
}
