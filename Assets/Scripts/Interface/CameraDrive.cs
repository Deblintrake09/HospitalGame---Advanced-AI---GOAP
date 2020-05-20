using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrive : MonoBehaviour{
	[SerializeField] private float speed=6f;
	[SerializeField] private float mouseSpeed=6f;
	[SerializeField] private float zoomSpeed=3f;
	[SerializeField] private float Rotspeed=20f;
	[SerializeField] private GameObject camera; 


	private void Update()
	{
		if ( Input.mousePosition.x >= Screen.width *0.98)
		{
			transform.Translate(Vector3.right *( Time.deltaTime * mouseSpeed), Space.Self);
		}
		if ( Input.mousePosition.x <= Screen.width *0.03)
		{
			transform.Translate(Vector3.right *( Time.deltaTime * -mouseSpeed), Space.Self);
		}
		if ( Input.mousePosition.y >= Screen.height *0.98)
		{
			transform.Translate(Vector3.forward * ( Time.deltaTime * mouseSpeed), Space.Self);
		}
		if ( Input.mousePosition.y <= Screen.height *0.03)
		{
			transform.Translate(Vector3.forward * ( Time.deltaTime * -mouseSpeed), Space.Self);
		}
		MoveCamera();
		RotateCamera();
		ZoomCamera();
	}

	private void MoveCamera()
	{
		Vector3 translation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) *
		                      (speed * Time.deltaTime);
		this.transform.Translate(translation);
	}

	private void RotateCamera()
	{
		float rotate = Input.GetAxis("RotateCamera") * Rotspeed * Time.deltaTime;
		this.transform.Rotate(0, rotate, 0);
	}

	private void ZoomCamera()
	{
		float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;

		if (zoom > 0 && camera.transform.position.y > 8)
			camera.transform.Translate(0, 0, zoom);
		if (zoom < 0 && camera.transform.position.y < 24)
			camera.transform.Translate(0, 0, zoom);
	}
}
