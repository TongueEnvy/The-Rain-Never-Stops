using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_rotate: MonoBehaviour {
    public float rotSpeed;

	private void FixedUpdate() {
        transform.eulerAngles = new Vector3(
			0f,
			0f,
			transform.eulerAngles.z + rotSpeed
		);
	}
}
