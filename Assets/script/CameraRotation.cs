using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	public float sensX = 100.0f;

	float rotationX = 0.0f;

	void Update () {


			rotationX += Input.GetAxis ("Horizontal") * sensX * Time.deltaTime;
			transform.localEulerAngles = new Vector3 (0, rotationX, 0);
		
	}
}
