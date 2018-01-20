using UnityEngine;

public class scr_rotate: MonoBehaviour {
    public float rotSpeed;
	
	// Update is called once per frame
	private void Update() {
        transform.eulerAngles = new Vector3(
			0,
			0,
			transform.eulerAngles.z + rotSpeed
		);
	}
}
