using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mindlessAI: MonoBehaviour {
    public GameObject bolt;
    public float shotTimer		=	6f;
    public float aggroRange		=	22f;
    public GameObject aggroMeter;
    public List<Sprite> aggroPhases;

	private GameObject target;
	private float shotCounter;
	private float distanceFromTarget;
	
	// Use this for initialization
	void Start() {
        shotCounter = shotTimer;
        target = GameObject.Find("playerHead");
        aggroMeter.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate() {

        distanceFromTarget =
			Vector2.Distance(transform.position, target.transform.position);

        if(distanceFromTarget <= aggroRange) {

            aggroMeter.SetActive(true);
            shotCounter -= Time.deltaTime;
            transform.LookAt(target.transform.position);
            transform.right = transform.forward;

            if (shotCounter < shotTimer / 4)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[3];

            }
            else if (shotCounter < (shotTimer / 4) * 2)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[2];

            }
            else if (shotCounter < (shotTimer / 4) * 3)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[1];

            }
            else if (shotCounter >= (shotTimer / 4) * 3)
            {

                aggroMeter.GetComponent<SpriteRenderer>().sprite = aggroPhases[0];

            }

            if (shotCounter <= 0) {
				var newBolt = Instantiate<GameObject>(bolt, transform);
				newBolt.transform.parent = transform.parent;
				newBolt.transform.LookAt(target.transform.position);
				newBolt.transform.right = newBolt.transform.forward;
				newBolt.GetComponent<CapsuleCollider2D>().direction	=
					CapsuleDirection2D.Vertical;
				newBolt.GetComponent<CapsuleCollider2D>().direction	=
					CapsuleDirection2D.Horizontal;

				shotCounter = shotTimer;
                
            }
        }
        else if(distanceFromTarget > aggroRange)
        {

            shotCounter = shotTimer;
            aggroMeter.SetActive(false);

        }
	}
}
