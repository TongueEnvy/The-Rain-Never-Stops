using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_pirouetteAI : MonoBehaviour {

    public float aggroTimer;
    float aggroCounter;
    public GameObject aggroMeter;
    public List<Sprite> aggroPhases;
    public GameObject daggar;
    public int daggarNumber;
    public AnimationClip idle;
    public AnimationClip throwLeft;
    public AnimationClip throwRight;
    public AnimationClip throwUpward;

    GameObject player;
    Animator anim;

	// Use this for initialization
	void Start () {

        aggroCounter = aggroTimer;
        player = GameObject.Find("playerHead");
        anim = gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        

        if (aggroCounter < aggroTimer / 4)
        {

            aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[3];

        }
        else if (aggroCounter < (aggroTimer / 4) * 2)
        {

            aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[2];

        }
        else if (aggroCounter < (aggroTimer / 4) * 3)
        {

            aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[1];

        }
        else if (aggroCounter >= (aggroTimer / 4) * 3)
        {

            aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[0];

        }

        aggroCounter -= Time.deltaTime;

        if(aggroCounter <= 0)
        {

           if (player.transform.position.x - transform.position.x >= 2)
           {

               ThrowRight();

           }
           else if (player.transform.position.x - transform.position.x <= -2)
           {

               ThrowLeft();

            }
            else
            {

                ThrowSkyward();

            }

            aggroCounter = aggroTimer;

        }

    }

        

    void ThrowRight()
    {

        anim.Play(throwRight.name);
        var throwDirection = 90f;
        for(var i = 0; i < 8; i += 1)
        {

            GameObject newDaggar = Instantiate<GameObject>(daggar);
            daggar.transform.eulerAngles = new Vector3(0, 0, 0);
            daggar.transform.eulerAngles = new Vector3(0, 0, throwDirection);
            daggar.transform.position = transform.position;
            throwDirection -= 15f;

        }

    }

    void ThrowLeft()
    {

        anim.Play(throwLeft.name);
        var throwDirection = 90f;
        for (var i = 0; i < 8; i += 1)
        {

            GameObject newDaggar = Instantiate<GameObject>(daggar);
            daggar.transform.eulerAngles = new Vector3(0, 0, 0);
            daggar.transform.eulerAngles = new Vector3(0, 0, throwDirection);
            daggar.transform.position = transform.position;
            throwDirection += 15f;

        }

    }

    void ThrowSkyward()
    {

        anim.Play(throwUpward.name);
        var throwDirection = 0f;
        for (var i = 0; i < 16; i += 1)
        {

            GameObject newDaggar = Instantiate<GameObject>(daggar);
            daggar.transform.eulerAngles = new Vector3(0, 0, 0);
            daggar.transform.eulerAngles = new Vector3(0, 0, throwDirection);
            daggar.transform.position = transform.position;
            throwDirection += (180 / 15);

        }

    }
}
