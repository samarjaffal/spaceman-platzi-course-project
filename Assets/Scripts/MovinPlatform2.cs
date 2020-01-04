using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovinPlatform2 : MonoBehaviour {

	public Transform p1,p2,startPosition;
	public float speed;

	Vector3 nextPosition;

	// Use this for initialization
	void Start () {
		nextPosition = startPosition.position;
	}
	
	// Update is called once per frame
	void Update () {
		MovePlatform();
	}

	void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player"){
			col.transform.parent = this.transform;
		}
    }

	private void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "Player"){
			col.transform.parent = null;
		}
	}

	void MovePlatform(){
		
		if(transform.position == p1.position){
			nextPosition = p2.position;
		}

		if(transform.position == p2.position){
			nextPosition = p1.position;
		}

		transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
	}

	private void OnDrawGizmos() {
		Gizmos.DrawLine(p1.position, p2.position);	
	}
}
