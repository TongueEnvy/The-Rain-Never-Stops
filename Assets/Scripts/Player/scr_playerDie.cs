using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class scr_playerDie : MonoBehaviour {

    public GameObject skull;
    public List<GameObject> bones;
    public List<GameObject> gibs;
    public float deathTimer;

	// Use this for initialization
	void Start () {

        gameObject.GetComponent<player_move>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        GameObject.Find("playerSegment0").SetActive(false);
        GameObject.Find("playerSegment1").SetActive(false);

        var newSkull = Instantiate<GameObject>(skull, null);
        newSkull.transform.position = transform.position;
        var newGlob = new GameObject(null);
        for(var i = 0; i < 10; i += 1)
        {

            newGlob = Instantiate<GameObject>(gibs[Random.Range(0, gibs.Count)], null);
            newGlob.transform.position = transform.position;
            newGlob.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4, 4), Random.Range(0, 4));

        }

	}
	
	// Update is called once per frame
	void Update () {

        deathTimer -= 1;

        if(deathTimer <= 0)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
	}
}
