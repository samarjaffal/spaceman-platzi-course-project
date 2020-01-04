using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("destroy the previous block");

		if(collision.tag == "Player"){
			LavelManager.sharedInstance.AddLevelBlock();
			LavelManager.sharedInstance.RemoveLevelBlock();
		}
	}
}
