using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset = new Vector3(0.2f, 0.2f, -10f);

	public float dampingTime = 0.3f;

	public float smoothSpeed = 0.125f;
	public Vector3 velocity = Vector3.zero;

	void Awake() {
		Application.targetFrameRate = 60; // set the frames for the game 60fps		
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MoveCamera(true);
	}

	public void ResetCameraPosition(){
		MoveCamera(false);
	}

	void MoveCamera(bool smooth){
		//Vector3 destination = new Vector3(target.position.x - offset.x, Mathf.Clamp(target.position.y, 1, -1) , offset.z);
		Vector3 destination = new Vector3(target.position.x - offset.x, target.position.y - offset.y , offset.z);
		if(smooth){
			// this.transform.position = Vector3.SmoothDamp(
			// 	this.transform.position, //the current position
			// 	destination, //destination or target
			// 	ref velocity, // ref to velocity 
			// 	dampingTime //the smoothtime or the efect slow
			// );
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, destination, smoothSpeed);
			transform.position = smoothedPosition;
		}else{
			this.transform.position = destination;
		}
	}
}