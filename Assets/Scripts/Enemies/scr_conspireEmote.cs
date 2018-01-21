using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class scr_conspireEmote : MonoBehaviour {
    
    //0 = neutral, 1 = laughing, 2 equals annoyed
    public int mood;
    public List<Sprite> emotes;
    public GameObject face;
    public GameObject player;

	// Use this for initialization
	void Start () {

        mood = 0;
        player = GameObject.Find("playerHead");

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject == player)
        {

            mood = 2;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == player)
        {

            mood = 0;

        }

    }

    // Update is called once per frame
    void Update () {

        face.GetComponent<SpriteRenderer>().sprite = emotes[mood];

    }
}
