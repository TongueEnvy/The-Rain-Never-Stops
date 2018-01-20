/*using UnityEngine;

//PlayerCameraControl inherits from MonoBehaviour (things like Update())
public class PlayerCollisionControl: MonoBehaviour {
	public GameObject slimeGlob;
	public int globNumber;
	public float globSpeed;

    private void OnTriggerEnter2D(Collider2D collision) {
        grounded = true;
        isJumping = false;
        
		if(collision.gameObject.tag == "Hazard") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        flungCounter = 0;

        hasBeenFlung = false;
    }
}*/