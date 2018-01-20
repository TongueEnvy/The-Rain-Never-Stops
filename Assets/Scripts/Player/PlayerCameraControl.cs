using UnityEngine;

//PlayerCameraControl inherits from MonoBehaviour (things like Update())
public class PlayerCameraControl: MonoBehaviour {
    public Camera cam;				//The main camera child GameObject.
    public float camSpeed			=	0.25f;	
	
	private bool isPlayerClimbing	=	false;
	private bool isCameraSet		=	false;
	
	//Allows isPlayerClimbing to be set by other functions without allowing
	//	it to be set directly in Unity or otherwise.
	public void SetPlayerClimbing(bool newState) { isPlayerClimbing = newState; }
	
	//Updates once a frame... Don't use FixedUpdate.
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
		else if((isPlayerClimbing == true) && (isCameraSet == false)) {
			cam.transform.position = Vector3.MoveTowards(
				cam.transform.position,
				new Vector3(0f, transform.position.y, -10f),
				camSpeed
			);
			
			//If the camera has reached the position it's moving towards.
			if(cam.transform.position.x == 0f) {
				isCameraSet = true;
			}
		}
		
		//Else the player is climbing and the camera has reached
		//	the center of the tower.
        else {
			cam.transform.position = new Vector3(0f, transform.position.y, -10);
        }
    }
}