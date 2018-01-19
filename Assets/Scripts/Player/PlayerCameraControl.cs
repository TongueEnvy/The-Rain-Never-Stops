using UnityEngine;

//PlayerCameraControl inherits from MonoBehaviour (things like Update())
public class PlayerCameraControl: MonoBehaviour {
    public Camera cam;				//The main camera child GameObject.
    public float camSpeed;			//The speed the camera moves to position.
	
	private bool isPlayerClimbing;	//Variable modified door trigger.
	
	//Allows isPlayerClimbing to be set by other functions without allowing
	//	it to be set directly in Unity or otherwise.
	public SetPlayerClimbing(bool newState) { isPlayerClimbing = newState; }
	
	//Update is called once per frame
    private void Update() {
		//If the player hasn't reached the door trigger...
		if(isPlayerClimbing == false) {
			//Allow camera to follow the player's x access.
            cam.transform.position = Vector3.MoveTowards(
				cam.transform.position, 
				new Vector3(transform.position.x, transform.position.y, -10f),
				camSpeed
			);
        }
		
		//Else if player has reached the door trigger, but the camera hasn't
		//	reached the center of the tower.
		else if((isPlayerClimbing == true) && (cam.transform.position.x != 0f)) {
			cam.transform.position = Vector3.MoveTowards(
				cam.transform.position,
				new Vector3(0f, transform.position.y, -10f),
				camSpeed
			);
		}
		
		//Else the player is climbing and the camera has reached
		//	the center of the tower.
        else {
            cam.transform.position = new Vector3(0, transform.position.y, -10);
        }
    }
}