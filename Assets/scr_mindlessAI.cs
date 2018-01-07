using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mindlessAI : MonoBehaviour {

    public GameObject target;
    public GameObject bolt;
    bool canShoot;
    public float shotTimer;
    float shotCounter;
    public float aggroRange;

	// Use this for initialization
	void Start () {

        shotCounter = shotTimer;
        target = GameObject.Find("playerHead");

	}
	
	// Update is called once per frame
	void Update () {

        if (shotCounter > 0)
        {

            shotCounter -= 1;

        }

        if (shotCounter <= 0)
        {

            canShoot = true;

        }

        if(Vector2.Distance(transform.position, target.transform.position) < aggroRange)
        {

            transform.LookAt(target.transform.position);
            transform.right = transform.forward;
            if (canShoot == true)
            {

                var newBolt = Instantiate<GameObject>(bolt, transform);
                newBolt.transform.position = transform.position;
                newBolt.transform.LookAt(target.transform.position);
                newBolt.transform.right = newBolt.transform.forward;
                newBolt.GetComponent<Rigidbody2D>().velocity = newBolt.transform.right * 2;
                newBolt.transform.parent = null;
                canShoot = false;
                shotCounter = shotTimer;

            }

        }
        



	}
}
