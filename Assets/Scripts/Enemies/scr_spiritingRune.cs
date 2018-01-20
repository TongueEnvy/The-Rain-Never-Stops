using UnityEngine;

public class scr_spiritingRune: MonoBehaviour {
    public GameObject weilder;
    public float launchForce;

    private Vector2 launchDirection;
	
	private void FixedUpdate() {
        launchDirection = transform.position - weilder.transform.position;
        launchDirection.Normalize();
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if(collision.gameObject.GetComponent<PlayerMove>() != null) {
                collision.gameObject.GetComponent<PlayerMove>().Fling(
					launchDirection * launchForce
				);
			}
		}
    }
}
