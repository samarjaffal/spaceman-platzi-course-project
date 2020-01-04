using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	public static PlayerController sharedInstance;
	public float jumpForce = 6f;
	public float runningSpeed = 3f;
	private Rigidbody2D rigidBody;

	private SpriteRenderer spriteRenderer;
	public LayerMask groundMask;

	public float groundDistance = 1.276363f;
	public Animator animator;

	private Vector3 startPosition;

	const string STATE_ALIVE =  "isAlive";
	const string STATE_ON_THE_GROUND = "isOnTheGround";

	const string STATE_IS_QUIET = "isQuiet";
	const string STATE_IS_WALKING = "isWalking";

	const string IS_PUNCHED = "isPunched";

	[SerializeField] private int healthPoins, manaPoints;
	public const int INITIAL_HEALTH = 100, INITIAL_MANA=15, 
	MAX_HEALTH = 200, MAX_MANA = 30,
	MIN_HEALTH = 10, MIN_MANA = 0;

	public const int SUPERJUMP_COST = 5;
	public const float SUPERJUMP_FORCE = 1.5f;

	public AudioSource jumpAudio, punchAudio;
	

	void Awake() {

		if(sharedInstance == null){
			sharedInstance = this;
		}
		
		rigidBody = GetComponent<Rigidbody2D>();
		animator  = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start () {
		startPosition = this.transform.position;
		var audioSources = GetComponents<AudioSource>();
		jumpAudio = audioSources[0];
		punchAudio = audioSources[1];
	}
	
	public void StartGame(){

		animator.SetBool(STATE_ALIVE, true);
		animator.SetBool(STATE_ON_THE_GROUND, true);
		animator.SetBool(STATE_IS_QUIET, true);
		animator.SetBool(STATE_IS_WALKING, false);
		
		healthPoins = INITIAL_HEALTH;
		manaPoints = INITIAL_MANA;
		Invoke("RestartPosition", 0.3f);
	}

	void RestartPosition(){
		this.transform.position = startPosition; 
		// we asign the actual position with the last position we saved in start function for restart
		this.rigidBody.velocity = Vector2.zero; 
		//is important reset the velocity to 0 becase when the player falls into the end,
		//the velocity increments
		GameObject maincamera = GameObject.Find("Main Camera");
		maincamera.GetComponent<CameraFollow>().ResetCameraPosition();
	}


	// Update is called once per frame
	void Update () {

		if(GameManager.sharedInstance.currentGameState == GameState.inGame){
			if(Input.GetButtonDown("Jump")){
				Jump(false);
			}
			if(Input.GetButtonDown("SuperJump")){
				Jump(true);
			}
		}
	
		animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround());

		Debug.DrawRay(this.transform.position, Vector2.down*1.4f, Color.red); // for debug purposes
	}

	void FixedUpdate() {
		// if(rigidBody.velocity.x < runningSpeed){
		// 	rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
		// }
		if(GameManager.sharedInstance.currentGameState == GameState.inGame){
			playerMovement();
		}
	}

	void playerMovement(){
		
		float isWalking = Input.GetAxisRaw("Horizontal");
	
		if(isWalking != 0){ //is true

			animator.SetBool(STATE_IS_QUIET, false);
			animator.SetBool(STATE_IS_WALKING, true);
			rigidBody.velocity = new Vector2(isWalking * runningSpeed,rigidBody.velocity.y);
			spriteRenderer.flipX = (isWalking == -1) ? true : false; 
			//if the value returns -1 that means is walking to the left and we need to make a flip in X
		}else{
			animator.SetBool(STATE_IS_QUIET, true);
			animator.SetBool(STATE_IS_WALKING, false);
		}
		
	}

	void Jump(bool superjump){

		float jumpForceFactor = jumpForce;

		if(superjump && manaPoints >= SUPERJUMP_COST){
			manaPoints -= SUPERJUMP_COST;
			jumpForceFactor *= SUPERJUMP_FORCE;
		}

		if(isTouchingTheGround()){
						
			animator.SetBool(STATE_IS_QUIET, false);
			animator.SetBool(STATE_IS_WALKING, false);
			jumpAudio.Play();
			rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);	
			//forcemode2d.impulse means is just a force that is not constante it's origined by impulse
		}else{
			animator.SetBool(STATE_IS_QUIET, true);
			animator.SetBool(STATE_IS_WALKING, false);
		}
	}

	//this function is bool beacause it returns a boolean value: true / false
	// helper to indicate if the player is touching or not the ground
	bool isTouchingTheGround(){ //0.2f is like 20cm
								// 1.5f is like 1m
		//raycast is a function to draw an inivisible line to detect if player is touching the ground
		// for that we create a new layer in unity called ground and set it to all prefabs we consider as a part of the ground 
		if(Physics2D.Raycast(this.transform.position, Vector2.down, groundDistance, groundMask)){
			return true;
		}else{
			return false;
		}
	}

	public void Die(){

		float travalledDistance = GetTravelledDistance();
		float previousDistance = PlayerPrefs.GetFloat("maxscore", 0); 
		PlayerPrefs.SetFloat("score", travalledDistance);
		if(travalledDistance > previousDistance){
			PlayerPrefs.SetFloat("maxscore", travalledDistance);
		}

		//Player.Prefs is for save in session like LocalStorage, some data for the player

		this.animator.SetBool(STATE_ALIVE, false);
		//this.animator.SetBool(STATE_IS_QUIET, false);
		GameManager.sharedInstance.GameOver();	
	}

	public void CollectHealth(int points){
		this.healthPoins += points;
		if(healthPoins >= MAX_HEALTH){
			this.healthPoins = MAX_HEALTH;
		}
		if(healthPoins <= 0){
			Die();
		}
	}

	public void CollectMana(int points){
		this.manaPoints += points;
		if(manaPoints >= MAX_MANA){
			this.manaPoints = MAX_MANA;
		}
	}

	public int GetHealth(){
		return healthPoins;
	}

	public int GetMana(){
		return manaPoints;
	}

	public float GetTravelledDistance(){
		return this.transform.position.x - startPosition.x;
	}
}
