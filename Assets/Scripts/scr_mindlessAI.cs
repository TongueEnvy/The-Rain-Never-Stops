using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mindlessAI: MonoBehaviour {
    public GameObject bolt;
    public float shotTimer		=	3f;
    public float aggroRange		=	22f;

	private GameObject target;
	private float shotCounter;
	private bool canShoot;
	private float distanceFromTarget;
	
	// Use this for initialization
	void Start() {
        shotCounter = shotTimer;
        target = GameObject.Find("playerHead");
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		distanceFromTarget =
			Vector2.Distance(transform.position, target.transform.position);

        if(distanceFromTarget < aggroRange) {
			shotCounter -= Time.deltaTime;
            transform.LookAt(target.transform.position);
            transform.right = transform.forward;

			if(shotCounter <= 0) {
				var newBolt = Instantiate<GameObject>(bolt, transform);
				newBolt.transform.parent = transform.parent;
				newBolt.transform.LookAt(target.transform.position);
				newBolt.transform.right = newBolt.transform.forward;
				newBolt.GetComponent<Rigidbody2D>().velocity = newBolt.transform.right * 2;

				shotCounter = shotTimer;
            }
        }
	}
}
