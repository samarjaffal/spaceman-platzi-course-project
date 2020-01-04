using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float runningSpeed = 1.5f;
	public int enemyDamage = 10;
	Rigidbody2D rigidBody;

	public bool facingRight = false;

	private Vector3 startPosition;
	private void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		startPosition = this.transform.position;
	}
	// Use this for initialization
	void Start () {
		// this.transform.position = startPosition;
	}
	
	//we use fixed update because it's called minor times than update, and it's more recomendated for
	//changing the physics, rigid body, forces, etc 
	private void FixedUpdate() {
		float currentRunningSpeed = runningSpeed;

		if(facingRight){
			//looking to right
			currentRunningSpeed = runningSpeed;
			GetComponent<SpriteRenderer>().flipX = true;
		}else{
			currentRunningSpeed = -runningSpeed;
			GetComponent<SpriteRenderer>().flipX = false;
		}

		if(GameManager.sharedInstance.currentGameState  == GameState.inGame){
			rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
			//GetComponent<AudioSource>().Play();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		if(collision.tag == "Coin"){
			return;
		}

		if(collision.tag == "Player"){
			collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
			collision.gameObject.GetComponent<PlayerController>().animator.SetTrigger("isPunched");
			collision.gameObject.GetComponent<PlayerController>().punchAudio.Play();
			return;
		}
		
		//if in case is not a player or the collision is not a coin, the most likely is that cashes
		//with an enemy ot the stage
		facingRight = !facingRight;
	}
}
